namespace HyBrForex.Infrastructure.Identity.Settings;
#pragma warning disable

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }
    public double RefreshTokenDurationInMinutes { get; set; }
}

#pragma warning disable

public class SMTPSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string EnableSsl { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ResetPasswordUrl { get; set; }
}