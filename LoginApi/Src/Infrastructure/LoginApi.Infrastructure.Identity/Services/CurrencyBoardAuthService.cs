using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Application.DTOs.Account.Responses;
using HyBrForex.Application.Helpers;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Models;
using HyBrForex.Infrastructure.Identity.Settings;
using LoginApi.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HyBrForex.Infrastructure.Identity.Services;

public class CurrencyBoardAuthService(
    UserManager<ApplicationUser> userManager,
    IAuthenticatedUserService authenticatedUser,
    SignInManager<ApplicationUser> signInManager,
    JwtSettings jwtSettings,
    ITranslator translator) : ICurrencyBoardAuthService
{
    public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
    {
        var user = await userManager.FindByIdAsync(authenticatedUser.UserId);

        var token = await userManager.GeneratePasswordResetTokenAsync(user!);
        var identityResult = await userManager.ResetPasswordAsync(user, token, model.Password);

        if (identityResult.Succeeded)
            return BaseResult.Ok();

        return identityResult.Errors.Select(p => new Error(ErrorCode.ErrorInIdentity, p.Description)).ToList();
    }

    public async Task<BaseResult<AuthenticationResponse>> AuthenticateByEmail(AuthenticationRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new Error(ErrorCode.NotFound,
                translator.GetString(TranslatorMessages.AccountMessages.Account_NotFound_with_UserName(request.Email)),
                nameof(request.Email));
        var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!signInResult.Succeeded)
            return new Error(ErrorCode.FieldDataInvalid,
                translator.GetString(TranslatorMessages.AccountMessages.Invalid_password()), nameof(request.Password));
        return await GetAuthenticationResponse(user);
    }

    private async Task<AuthenticationResponse> GetAuthenticationResponse(ApplicationUser user)
    {
        await userManager.UpdateSecurityStampAsync(user);

        var jwToken = await GenerateJwtToken();

        var rolesList = await userManager.GetRolesAsync(user);

        return new AuthenticationResponse
        {
            Id = user.Id,
            JwToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
            Email = user.Email,
            UserName = user.UserName,
            Roles = rolesList,
            IsVerified = user.EmailConfirmed
        };

        async Task<JwtSecurityToken> GenerateJwtToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                (await signInManager.ClaimsFactory.CreateAsync(user)).Claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
        }
    }
}