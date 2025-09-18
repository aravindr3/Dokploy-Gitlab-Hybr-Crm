using System.Collections.Generic;

namespace HyBrForex.Application.DTOs.Account.Responses;
public class AuthenticationResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public bool IsVerified { get; set; }
    public string JwToken { get; set; }
    public string RefreshToken { get; set; }
    public bool RequiresTwoFactor { get; set; }
    public string VerticalId { get; set; }
    public string VerticalName { get; set; }
    public string ? DomainId { get; set; }
    public string ? DomainName { get; set; }
    public IList<string> Branches { get; set; }
}