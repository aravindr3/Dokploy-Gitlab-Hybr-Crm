using System;
using Microsoft.AspNetCore.Identity;

namespace LoginApi.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public override string Id { get; set; } = Ulid.NewUlid().ToString();
        public string VerticalId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string TwoFactorSecretKey { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime ResetPasswordExpirationTime { get; set; }


    }
}
