using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class ReportMapping : AuditableBaseEntity
    {
        public string ReportId { get; set; }
        public string RoleId { get; set; }
    }
}
