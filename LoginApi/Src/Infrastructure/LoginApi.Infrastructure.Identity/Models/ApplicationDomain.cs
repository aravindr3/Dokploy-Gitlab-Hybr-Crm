using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Infrastructure.Identity.Models
{
    public class ApplicationDomain : AuditableBaseEntity
    {
        public string ? DomaninName { get; set; }
    }
}
