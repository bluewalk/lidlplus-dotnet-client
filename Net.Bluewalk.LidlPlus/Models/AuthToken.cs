using System;
using Newtonsoft.Json;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class AuthToken
    {
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        public DateTime ExpiresAt { get; set; }

        public AuthToken()
        {
            ExpiresAt = DateTime.Now.AddSeconds(ExpiresIn);
        }
    }
}