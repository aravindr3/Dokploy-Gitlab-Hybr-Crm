using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class ApplicationLoginInfo
    {
        public string Id { get; set; } = Ulid.NewUlid().ToString();
        public string UserId { get; set; }
        public string SessionToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryTime { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsRevoked { get; set; } = false;
    }
}
