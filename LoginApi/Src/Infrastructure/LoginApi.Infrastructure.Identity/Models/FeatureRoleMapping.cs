using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class FeatureRoleMapping : AuditableBaseEntity
    {
        public string FeatureId { get; set; }
        public string FunctionalId { get; set; }
        public string RoleId { get; set; }
    }
}
