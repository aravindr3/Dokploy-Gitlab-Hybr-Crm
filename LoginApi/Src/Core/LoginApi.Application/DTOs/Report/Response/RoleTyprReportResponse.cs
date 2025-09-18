using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Report.Request;

namespace HyBrForex.Application.DTOs.Report.Response
{
    public class RoleTyprReportResponse
    {
        public string RoleId { get; set; }
        public List<ReportTypeDto> Permissions { get; set; }
       
    }
    public class ReportTypeDto
    {
        public string ReportId { get; set; }
        public bool Assign { get; set; }
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

        public string? ParentId { get; set; }
        public required string Name { get; set; }
    }
}
