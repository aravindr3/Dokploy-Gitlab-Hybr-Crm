using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class ReportRoleMapping : AuditableBaseEntity
    {
        public string ReportFeatureID { get; set; }
        public string ReportID { get; set; }
        public string RoleId { get; set; }
    }
}
