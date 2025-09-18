using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class ReportType : AuditableBaseEntity
    {
        public string ReportID { get; set; }
        public string Type { get; set; }
        public string Query { get; set; }
        public string Align { get; set; }

    }
  
}
