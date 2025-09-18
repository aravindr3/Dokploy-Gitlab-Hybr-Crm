using Microsoft.AspNetCore.Identity;

namespace HyBrForex.Infrastructure.Identity.Models;

public class ApplicationRole(string name) : IdentityRole<string>(name)
{
}