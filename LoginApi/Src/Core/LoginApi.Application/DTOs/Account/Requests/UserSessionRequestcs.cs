using System;

namespace HyBrForex.Application.DTOs.Account.Requests;

public class UserSessionRequest
{
    public string Id { get; set; }
    public string SessionToken { get; set; }
    public string RefreshToken { get; set; }
    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
    public DateTime LoginTime { get; set; }
    public DateTime LastActivity { get; set; }
    public DateTime ExpiryTime { get; set; }
    public bool IsActive { get; set; }
    public bool IsRevoked { get; set; }
}