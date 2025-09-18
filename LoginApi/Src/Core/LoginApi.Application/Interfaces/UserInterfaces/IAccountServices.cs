using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Application.DTOs.Account.Responses;
using HyBrForex.Application.Wrappers;

namespace LoginApi.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<AuthorizeResponse> AuthorizeUserAsync(AuthorizeRequest request);
        Task<BaseResult<string>> RegisterGhostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeEmail(ChangeEmailRequest model);
        Task<BaseResult<HyBrForex.Application.DTOs.Account.Responses.TwoFactorResponse>> Authenticate(AuthenticationRequest request);
        Task<BaseResult<AuthenticationResponse>> AuthenticateByEmail(string Email);
        Task<BaseResult<string>> EnableTwoFactorAuthentication(string id);
        Task<BaseResult> UpdateTwoFactorStatus(string userId, bool isEnabled);
        Task<BaseResult<AuthenticationResponse>> VerifyTwoFactorCode(string Id, string otpCode);
        Task<BaseResult<AuthenticationResponse>> VerifyTwoFactorCodewithconcurrent(string Id, string otpCode, bool forceLogin);
        
        Task<BaseResult> DeActivateTwoFactorStatus(string userId);
        Task<BaseResult<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request);
        Task<BaseResult> SendResetPasswordLink(PasswordResetRequest request, string emailTemplatePath);
        Task<BaseResult> ResetPassword(HyBrForex.Application.DTOs.Account.Requests.ResetPasswordRequest request);
        Task<BaseResult> ValidateResetToken(ValidateResetTokenRequest request);
        Task<bool> CheckConcurrentUser(string userId);
        Task<bool> UpdateUserSession(UserSessionRequest sessionRequest);
        Task<bool> TerminateOtherSessions(string userId);
        Task<bool> DeactivateSessionToken(string token);
        Task<bool> ValidateJwtToken(string token);
        Task<List<GetBranchByIdResponse>> GetBranchNamesAsync(List<string> branchIds);
    }
}
