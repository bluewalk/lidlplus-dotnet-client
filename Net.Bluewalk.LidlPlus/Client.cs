using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using Net.Bluewalk.LidlPlus.Models;
using Newtonsoft.Json;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ZXing;
using ZXing.Common;

namespace Net.Bluewalk.LidlPlus
{
    public class Client
    {
        private static string ACCOUNT_URL = "https://accounts.lidl.com/";
        private static string APPGATEWAY_URL = "https://appgateway.lidlplus.com/app/v19/NL/";

        private static object REQUEST_HEADERS = new
        {
            App_Version = "999.99.9",
            Operating_System = "iOS",
            App = "com.lidl.eci.lidl.plus",
            Accept_Language = "nl_NL"
        };
        private static string TOKEN_PATH = Path.Combine(Path.GetTempPath(), "net-bluewalk-lidl-token.json");

        private readonly string _refreshToken;
        private readonly IWebProxy _webProxy;
        private AuthToken _authToken;

        public Client(string refreshToken, IWebProxy webProxy = null)
        {
            _refreshToken = refreshToken;
            _webProxy = webProxy;

            if (File.Exists(TOKEN_PATH))
                _authToken = JsonConvert.DeserializeObject<AuthToken>(File.ReadAllText(TOKEN_PATH));
        }

        private IFlurlClient GetClient(string baseUrl)
        {
            return new FlurlClient(baseUrl).Configure(s =>
            {
                s.HttpClientFactory = new ProxyHttpFactory(_webProxy);
                s.JsonSerializer = new NewtonsoftJsonSerializer(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        Culture = new CultureInfo("nl-NL")
                    });
            });
        }

        private IFlurlRequest GetRequest(string url)
        {
            return url
                .WithClient(GetClient(APPGATEWAY_URL))
                .WithHeaders(REQUEST_HEADERS)
                .WithOAuthBearerToken(_authToken.AccessToken);
        }

        private async Task Auth()
        {
            _authToken = await "connect/token"
                .WithClient(GetClient(ACCOUNT_URL))
                .WithBasicAuth("LidlPlusNativeClient", "secret")
                .PostUrlEncodedAsync(new
                {
                    refresh_token = _refreshToken,
                    grant_type = "refresh_token"
                })
                .ReceiveJson<AuthToken>();

            File.WriteAllText(TOKEN_PATH, JsonConvert.SerializeObject(_authToken));
        }

        private async Task CheckAuth()
        {
            if (_authToken == null || _authToken.ExpiresAt < DateTime.Now.AddMinutes(1))
                await Auth();
        }

        public async Task<Store> GetStore(string code)
        {
            await CheckAuth();

            return await GetRequest($"stores/{code}")
                .AllowAnyHttpStatus()
                .GetJsonAsync<Store>();
        }

        public async Task<List<Ticket>> GetTickets()
        {
            await CheckAuth();

            return await GetRequest("tickets")
                .AllowAnyHttpStatus()
                .GetJsonAsync<List<Ticket>>();
        }

        public async Task<Ticket> GetTicket(string id)
        {
            await CheckAuth();

            return await GetRequest($"tickets/{id}")
                .AllowAnyHttpStatus()
                .GetJsonAsync<Ticket>();
        }

        public async Task<TicketString> GetTicketString(string id)
        {
            var ticket = await GetTicket(id);
            var store = await GetStore(ticket.StoreCode);

            var format = "{0, -45}{1, 5}\r\n";

            var header = new StringBuilder();
            header.AppendLine(store.Address.PadBoth(50));
            header.AppendLine($"{store.PostalCode} {store.Locality}".PadBoth(50));

            var contents = new StringBuilder();
            contents.AppendLine($"{"OMSCHRIJVING",-45}{"EUR",5}");

            ticket.ItemsLine.ForEach(l =>
            {
                contents.AppendFormat(format, l.Description, l.OriginalAmount);

                if (l.Quantity != 1)
                    if (l.IsWeight)
                        contents.AppendLine($"    {l.Quantity} kg x {l.CurrentUnitPrice} EUR/kg");
                    else
                        contents.AppendLine($"    {l.Quantity} X {l.CurrentUnitPrice}");

                if (l.Deposit != null)
                {
                    contents.AppendFormat(format, l.Deposit.Description, l.Deposit.Amount);
                    contents.AppendLine($"    {l.Deposit.Quantity} X {l.Deposit.UnitPrice}");
                }

                l.Discounts.ForEach(d => contents.AppendFormat("   {0, -42}{1, 5}", d.Description, d.Amount));
            });

            // Totals
            contents.AppendFormat("{0,50}\r\n", "------------");
            contents.AppendFormat("Te betalen{0, 20}{1, 20}\r\n", $"{ticket.LinesScannedCount} art",
                ticket.TotalAmount);
            contents.AppendFormat("{0,50}\r\n", "============");

            contents.AppendFormat(format, ticket.Payments.FirstOrDefault()?.Description,
                ticket.Payments.FirstOrDefault()?.Amount);

            // Payment info
            contents.AppendLine();
            contents.AppendLine("--------------------------------------------------\r\n");
            contents.AppendLine(ticket.Payments?.FirstOrDefault()?.RawPaymentInformationHtml.Br2Nl().StripTags().Trim());
            contents.AppendLine();

            // Taxes
            contents.AppendLine("%              Bedr.Excl         BTW     Bedr.Incl");
            ticket.Taxes.ForEach(t => contents.AppendFormat("{0, -9}{1, 15}{2, 12}{3, 14}\r\n", t.Percentage, t.NetAmount, t.Amount, t.TaxableAmount));
            contents.AppendLine("--------------------------------------------------");
            contents.AppendFormat("Som      {0, 15}{1, 12}{2, 14}", ticket.TotalTaxes.TotalNetAmount,
                ticket.TotalTaxes.TotalAmount, ticket.TotalTaxes.TotalTaxableAmount);

            var footer = new StringBuilder();
            footer.AppendFormat("{0, -9}{1, 10}{2, 17}{3, 14}", ticket.StoreCode.Substring(2),
                $"{ticket.SequenceNumber}/{ticket.Workstation}", $"{ticket.Date:dd.MM.yy}", $"{ticket.Date:HH:mm}");

            return new TicketString
            {
                Body = contents.ToString(),
                Footer = footer.ToString(),
                Header = header.ToString(),
                Ticket = ticket
            };
        }

        public async Task<byte[]> GetTicketImage(string id, ImageType type = ImageType.Jpg)
        {
            var str = await GetTicketString(id);

            var collection = new FontCollection();
            var family = collection.Install(typeof(Client).GetTypeInfo().Assembly
                .GetManifestResourceStream("Net.Bluewalk.LidlPlus.Resources.receipt_font.ttf"));
            var font = family.CreateFont(12);

            var measurement = TextMeasurer.Measure(str.Body, new RendererOptions(font));
            var width = (int) measurement.Width + 40;
            var height = (int) measurement.Height + 40 + 170 /* logo */ + 130 /* barcode */;

            var logo = Image.Load(typeof(Client).GetTypeInfo().Assembly
                .GetManifestResourceStream("Net.Bluewalk.LidlPlus.Resources.logo.png"));

            var bcw = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.ITF,
                Options = new EncodingOptions
                {
                    Height = 75
                }
            };
            var barcode = bcw.Write(str.Ticket.BarCode);

            using (var receipt = new Image<Rgba32>(width, height))
            {
                receipt.Mutate(x => x.BackgroundColor(Color.White));

                // Logo
                receipt.Mutate(x => x.DrawImage(logo, new Point((width / 2 - logo.Width / 2), 20), 1));
                var pos = 110;

                // Header
                receipt.Mutate(x => x.DrawText(str.Header, font, Color.Black, new PointF(20, pos)));
                pos += 60;

                // Body
                receipt.Mutate(x => x.DrawText(str.Body, font, Color.Black, new PointF(20, pos)));
                pos += (int) measurement.Height + 20;

                // add barcode
                receipt.Mutate(x =>
                    x.DrawImage(Image.LoadPixelData<Rgba32>(barcode.Pixels, barcode.Width, barcode.Height),
                        new Point(width / 2 - barcode.Width / 2, pos), 1));
                pos += 90;

                // Footer
                receipt.Mutate(x => x.DrawText(str.Footer, font, Color.Black, new PointF(20, pos)));

                using (var ms = new MemoryStream())
                {
                    switch (type)
                    {
                        case ImageType.Png:
                            receipt.SaveAsPng(ms);
                            break;
                        case ImageType.Bmp:
                            receipt.SaveAsBmp(ms, new BmpEncoder
                            {
                                BitsPerPixel = BmpBitsPerPixel.Pixel32
                            });
                            break;
                        default:
                            receipt.SaveAsJpeg(ms, new JpegEncoder
                            {
                                Quality = 100
                            });
                            break;
                    }

                    return ms.ToArray();
                }
            }
        }
    }

    public class ProxyHttpFactory : DefaultHttpClientFactory
    {
        private readonly IWebProxy _webProxy;

        public ProxyHttpFactory(IWebProxy webProxy)
        {
            _webProxy = webProxy;
        }
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                Proxy = _webProxy
            };
        }
    }
}
