using System.Security.Claims;
using HyBrForex.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HyBrForex.WebApi.Infrastructure.Services;

public class AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) : IAuthenticatedUserService
{
    public string UserId { get; } = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string UserName { get; } = httpContextAccessor.HttpContext?.User.Identity?.Name;
}