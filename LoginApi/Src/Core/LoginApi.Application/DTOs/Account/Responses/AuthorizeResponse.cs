namespace HyBrForex.Application.DTOs.Account.Responses;

public class AuthorizeResponse
{
    public bool IsAuthorized { get; set; }
    public string Message { get; set; } = string.Empty;
}