using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;
using LoginApi.Infrastructure.Identity.Models;

namespace HyBrCRM.Infrastructure.Identity.Models
{
    public class Vertical : AuditableBaseEntity
    {
        public string ? TenantId { get; set; }
        public string ? DomainId { get; set; }
        public string ? VerticalName { get; set; }

        public virtual ApplicationTenant ? Tenant { get; set; }
        public virtual ApplicationDomain ? Domain { get; set; }
    }
}
