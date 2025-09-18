namespace HyBrForex.Application.DTOs.Account.Requests;

public class EnableTwoFactorRequest
{
    public string Id { get; set; }
}

public class UpdateTwoFactorStatusRequest
{
    public string UserId { get; set; }
    public bool IsEnabled { get; set; }
}

public class DeActivateTwoFactorStatusRequest
{
    public string UserId { get; set; }
}