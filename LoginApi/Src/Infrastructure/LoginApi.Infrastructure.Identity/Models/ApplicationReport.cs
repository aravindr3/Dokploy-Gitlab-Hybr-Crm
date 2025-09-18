using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class ApplicationReport : AuditableBaseEntity
    {
        public string ReportFeatureName { get; set; }
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

    }
}
