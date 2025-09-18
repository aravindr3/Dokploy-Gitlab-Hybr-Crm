using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity.Settings;
using LoginApi.Application.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HyBrForex.WebApi.Infrastructure.Filters;

public class AuthenticationFilter(IAccountServices accountServices) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].ToString();
        token = token.Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (await accountServices.ValidateJwtToken(token))
            await Task.CompletedTask;
        else
            context.Result = new UnauthorizedResult();
    }

}