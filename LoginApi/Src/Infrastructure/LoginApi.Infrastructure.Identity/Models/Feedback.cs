using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class Feedback : AuditableBaseEntity
    {
        public string PageTitle { get; set; }
        public string FeedbackType { get; set; } // "suggestion" or "bug"
        public string Email { get; set; }
        public string Comments { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
