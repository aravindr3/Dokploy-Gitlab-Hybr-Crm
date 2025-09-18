using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Report.Response
{
    public class ReportRoleMappingResponse
    {
        public String Id { get; set; }
        public string ReportId { get; set; } = default!;
        public bool Csv { get; set; }
        public bool Excel { get; set; }
        public bool PDF { get; set; }
        public bool Print { get; set; }
        public bool Branch { get; set; }
        public bool Currency { get; set; }
        public bool Transaction { get; set; }
        public bool ReportOption { get; set; }
        public bool DateRange { get; set; }
        public bool IncludeCharts { get; set; }
        public List<string> RoleIds { get; set; }

    }
}
