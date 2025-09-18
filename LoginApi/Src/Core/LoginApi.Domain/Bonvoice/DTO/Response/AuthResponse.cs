using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HyBrCRM.Domain.Bonvoice.DTO.Response
{
    public class AuthResponse
    {
        
    public string Status { get; set; }
        public AuthResponseData Data { get; set; }
        public string ? AuthId { get; set; }
    }

    public class AuthResponseData
    {
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("header key")]
        public string HeaderKey { get; set; }

        [JsonProperty("header_value")]
        public string HeaderValue { get; set; }
    }
}

