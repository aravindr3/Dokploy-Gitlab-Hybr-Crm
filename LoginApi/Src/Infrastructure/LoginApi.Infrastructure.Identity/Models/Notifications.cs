using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class Notifications : AuditableBaseEntity
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
