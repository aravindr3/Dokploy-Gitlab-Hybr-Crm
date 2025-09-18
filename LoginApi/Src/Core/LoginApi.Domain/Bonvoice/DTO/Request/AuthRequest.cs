using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HyBrCRM.Domain.Bonvoice.DTO.Request
{
    public class AuthRequest
    {
        public string ? LeadId { get; set; }
        public string ? TaskId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
