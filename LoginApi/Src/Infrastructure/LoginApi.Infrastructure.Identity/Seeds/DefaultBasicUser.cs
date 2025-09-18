using System.Threading.Tasks;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace HyBrForex.Infrastructure.Identity.Seeds;

public static class DefaultBasicUser
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new ApplicationUser
        {
            VerticalId = "01JMKB31YXGRF2H9K7XJC8EATQ",
            UserName = "MediaUser",
            Email = "css@alhindonline.com",
            Name = "MediaUser",
            PhoneNumber = "9446000445",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        //if (!await userManager.Users.AnyAsync())
        //{
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if (user == null)
        {
            var _user = await userManager.CreateAsync(defaultUser, "MediaUser@123");

            user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (!await roleManager.RoleExistsAsync("MediaUser"))
                await roleManager.CreateAsync(new ApplicationRole("MediaUser"));
            var role = await roleManager.FindByNameAsync("MediaUser");

            if (role != null)
            {
                var result = await userManager.AddToRoleAsync(user, role.Name);
            }
        }
        //}
    }
}