using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HyBrForex.Application.DTOs.Report.Request
{
    public class ReportQueryRequest
    {
        [JsonProperty("reportIdOrName")]
        public string ReportIdOrName { get; set; }

        [JsonProperty("Filters")]
        public Dictionary<string, object> Filters { get; set; } = new();
    }
}
