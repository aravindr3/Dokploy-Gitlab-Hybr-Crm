using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Account.Requests;
using HyBrForex.Application.DTOs.Account.Responses;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.Application.Interfaces.UserInterfaces;

public interface ICurrencyBoardAuthService
{
    Task<BaseResult> ChangePassword(ChangePasswordRequest model);

    Task<BaseResult<AuthenticationResponse>> AuthenticateByEmail(AuthenticationRequest request);
}