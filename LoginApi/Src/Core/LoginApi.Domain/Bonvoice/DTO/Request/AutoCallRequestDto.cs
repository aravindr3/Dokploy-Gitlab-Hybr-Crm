using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HyBrCRM.Domain.Bonvoice.DTO.Request
{
    public class AutoCallRequestDto
    {
        public AutoCallRequestDto()
        {
        }
       
        [JsonProperty("autocallType")]
        public string AutocallType { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("ringStrategy")]
        public string RingStrategy { get; set; }

        [JsonProperty("legACallerID")]
        public string LegACallerID { get; set; }

        [JsonProperty("legAChannelID")]
        public string LegAChannelID { get; set; }

        [JsonProperty("legADialAttempts")]
        public string LegADialAttempts { get; set; }

        [JsonProperty("legBDestination")]
        public string LegBDestination { get; set; }

        [JsonProperty("legBCallerID")]
        public string LegBCallerID { get; set; }

        [JsonProperty("legBChannelID")]
        public string LegBChannelID { get; set; }

        [JsonProperty("legBDialAttempts")]
        public string LegBDialAttempts { get; set; }

        [JsonProperty("eventID")]
        public string EventID { get; set; }
    }
}
