using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Report.Request
{
    public class GetReportByIdRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Query { get; set; }
    }
}
