using System.Threading.Tasks;
using HyBrForex.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HyBrForex.Infrastructure.Identity.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
    {
        //Seed Roles
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new ApplicationRole("Admin"));
    }
}