using System.Collections.Generic;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
using Newtonsoft.Json;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Response
{
    public class CurrencyResponse
    {
        public List<CurrencyProperties> currencies { get; set; }
    }

    public class CurrencyBoardResponse
    {
        public List<CurrencyBoardProperties> currencies { get; set; }
        public string? CurrencyDate { get; set; }
    }

    public class CurrencyRateUpdaterResponse
    {
        public string? result { get; set; }
        public string? documentation { get; set; }
        public string? terms_of_use { get; set; }
        public long time_last_update_unix { get; set; }
        public string? time_last_update_utc { get; set; }
        public long time_next_update_unix { get; set; }
        public string? time_next_update_utc { get; set; }
        public string? disclaimer { get; set; }
        public string? license { get; set; }
        public long? timestamp { get; set; }

        [JsonProperty(PropertyName = "base")] public string? curbase { get; set; }

        public Dictionary<string, double> rates { get; set; }
        public Dictionary<string, double> conversion_rates { get; set; }
    }
}