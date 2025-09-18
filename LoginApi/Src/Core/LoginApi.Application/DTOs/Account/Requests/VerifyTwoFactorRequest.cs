namespace HyBrForex.Application.DTOs.Account.Requests;

public class VerifyTwoFactorRequest
{
    public string Id { get; set; }
    public string otpCode { get; set; }
    public bool ForceLogin { get; set; }
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}

public class PasswordResetRequest
{
    public string Email { get; set; }
}

public class ResetPasswordRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

public class ValidateResetTokenRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string ResetPasswordExpirationTime { get; set; } = string.Empty;
}

public class ValidateJwtTokenRequest
{
    public string Token { get; set; }
}

public class DeactivateTokenRequest
{
    public string UserId { get; set; }
    public string SessionToken { get; set; }
}