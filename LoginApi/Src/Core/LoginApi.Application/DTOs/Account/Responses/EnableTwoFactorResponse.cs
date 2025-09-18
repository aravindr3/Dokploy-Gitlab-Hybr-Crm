namespace HyBrForex.Application.DTOs.Account.Responses;

public class EnableTwoFactorResponse
{
    public string QrCodeUrl { get; set; }
    public string ManualKey { get; set; }
}

public class RefreshTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class PasswordResetResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}