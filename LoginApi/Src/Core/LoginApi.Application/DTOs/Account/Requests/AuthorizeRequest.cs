namespace HyBrForex.Application.DTOs.Account.Requests;

public class AuthorizeRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}