using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Report.Response
{
    public class ReportQueryResponse
    {
        public List<Dictionary<string, object>> Records { get; set; } = new();
    }
}
