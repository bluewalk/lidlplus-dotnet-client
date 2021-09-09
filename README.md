# lidlplus-dotnet-client
Lidl Plus .NET API client

![Publish NuGet](https://github.com/bluewalk/lidlplus-dotnet-client/workflows/Publish%20NuGet/badge.svg)

This library allows you to query your Lidl Plus receipts, coupons and stores.
It even allows you to generate a PNG receipt to use in your automation scripts, e.g. automatically add a receipt to your transactions.

## NuGet Package
This library is available as a [NuGet package](https://www.nuget.org/packages/Net.Bluewalk.LidlPlus).

Run `Install-Package Net.Bluewalk.LidlPlus` in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console) or search for "LidlPlus" in your IDE's package management plug-in.


## How to use
First of all you need to get your `refresh_token` from your app. To do this you need to set up a MITM proxy and capture traffic.
You can do this with Fiddler or Proxie. Install the CA certificate on your phone, start capture and start your Lidl Plus app.
You'll see a request going to `https://accounts.lidl.com/connect/token` which will use your `refresh_token`.

```csharp
var client = new Client("myrefreshtoken");
var receipts = await client.GetTickets();

var latest = await client.GetTicketImage(receipts.Records.FirstOrDefault().Id);

File.WriteAllBytes("receipt.png", latest);
```
