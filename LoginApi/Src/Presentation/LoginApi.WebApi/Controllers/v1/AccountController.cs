using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Application.DTOs.Account.Responses;
using HyBrForex.Application.Wrappers;
using HyBrForex.WebApi.Controllers;
using HyBrForex.WebApi.Infrastructure.Filters;
using LoginApi.Application.Interfaces.UserInterfaces;
using LoginApi.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class AccountController(IAccountServices accountServices) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<HyBrForex.Application.DTOs.Account.Responses.TwoFactorResponse>> Authenticate(AuthenticationRequest request)
            => await accountServices.Authenticate(request);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangeUserName(ChangeEmailRequest model)
            => await accountServices.ChangeEmail(model);

        [HttpPut, Authorize]
        public async Task<BaseResult> ChangePassword(ChangePasswordRequest model)
            => await accountServices.ChangePassword(model);

        [HttpPost("verify")]
        public async Task<BaseResult> VerifyAuthorization([FromBody] AuthorizeRequest request)
        {
            var response = await accountServices.AuthorizeUserAsync(request);
            return response.IsAuthorized ? BaseResult.Ok() : BaseResult.Failure(new Error(ErrorCode.NotFound, "Unauthorized"));
        }

        [HttpPost]
        public async Task<BaseResult<AuthenticationResponse>> Start()
        {
            var ghostEmail = await accountServices.RegisterGhostAccount();
            return await accountServices.AuthenticateByEmail(ghostEmail.Data);
        }

        [HttpPost("enable-2fa")]
        public async Task<BaseResult> Enable2FA([FromBody] EnableTwoFactorRequest request)
            => await accountServices.EnableTwoFactorAuthentication(request.Id);

        [HttpPost("verify-2fa")]
        public async Task<BaseResult> Verify2FA([FromBody] VerifyTwoFactorRequest request)
            => await accountServices.VerifyTwoFactorCode(request.Id, request.otpCode);

        [HttpPost("verify-2fa-Concurrent")]
        public async Task<BaseResult> Verify2FAwithConcurrent([FromBody] VerifyTwoFactorRequest request)
            => await accountServices.VerifyTwoFactorCodewithconcurrent(request.Id, request.otpCode, request.ForceLogin);

        [HttpPost("update-2fa-status")]
        public async Task<BaseResult> UpdateTwoFactorStatus([FromBody] UpdateTwoFactorStatusRequest request)
            => await accountServices.UpdateTwoFactorStatus(request.UserId, request.IsEnabled);

        [HttpPost("refresh-token")]
        public async Task<BaseResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
            => await accountServices.RefreshToken(request);

        [HttpPost("DeActivate-2fa-status")]
        public async Task<BaseResult> DeActivateTwoFactorStatus([FromBody] DeActivateTwoFactorStatusRequest request)
            => await accountServices.DeActivateTwoFactorStatus(request.UserId);

        [HttpPost("send-reset-password-link")]
        public async Task<BaseResult> SendResetPasswordLink([FromBody] PasswordResetRequest request)
        {
            var emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "ResetPasswordTemplate.html");
            return await accountServices.SendResetPasswordLink(request, emailTemplatePath);
        }

        [HttpPost("validate-reset-token")]
        public async Task<BaseResult> ValidateResetToken([FromBody] ValidateResetTokenRequest request)
            => await accountServices.ValidateResetToken(request);

        [HttpPost("reset-password")]
        public async Task<BaseResult> ResetPassword([FromBody] HyBrForex.Application.DTOs.Account.Requests.ResetPasswordRequest request)
            => await accountServices.ResetPassword(request);

        [HttpPost("terminate-session")]
        public async Task<BaseResult> TerminateOtherSessions([FromBody] VerifyTwoFactorRequest request)
        {
            var result = await accountServices.TerminateOtherSessions(request.Id);
            return result ? BaseResult.Ok() : BaseResult.Failure(new Error(ErrorCode.NotFound, "Failed to terminate session"));
        }

        [HttpPost("deactivate-session")]
        public async Task<BaseResult> DeactivateSession([FromBody] DeactivateTokenRequest request)
        {
            var success = await accountServices.DeactivateSessionToken(request.SessionToken);
            return success ? BaseResult.Ok() : BaseResult.Failure(new Error(ErrorCode.NotFound, "Session not found or already deactivated"));
        }

        [HttpPost("get-branch-names")]
        public async Task<IActionResult> GetBranchNames([FromBody] GetBranchByIdRequest request)
        {
            if (request.BranchIds == null || !request.BranchIds.Any())
            {
                return BadRequest("Branch IDs are required.");
            }

            var branches = await accountServices.GetBranchNamesAsync(request.BranchIds);

            if (branches.Count == 0)
            {
                return NotFound("No branches found for the given IDs.");
            }

            return Ok(branches);
        }
    }


}
