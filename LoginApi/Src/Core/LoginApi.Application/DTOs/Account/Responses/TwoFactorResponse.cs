namespace HyBrForex.Application.DTOs.Account.Responses;

public class TwoFactorResponse
{
    public string Email { get; set; }
    public string Id { get; set; }
    public bool RequiresTwoFactor { get; set; }
}