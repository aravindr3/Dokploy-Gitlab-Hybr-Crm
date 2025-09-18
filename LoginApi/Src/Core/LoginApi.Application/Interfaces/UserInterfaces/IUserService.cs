using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.User.Requests;
using HyBrForex.Application.DTOs.User.Responses;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<BaseResult<UserResponse>> CreateUserAsync(UserRequest request);
        Task<BaseResult<UserResponse>> UpdateUserAsync(string userId, UserRequest request);
        Task<BaseResult<bool>> DeleteUserAsync(string userId);
        Task<BaseResult<IEnumerable<GetUserResponse>>> GetAllUsersAsync();
        Task<BaseResult<GetUserResponse?>> GetUserByIdAsync(string userId);
        Task<BaseResult<List<GetBranchByTenantResponse>>> GetBranchByTenantAsync(GetBranchByTenantRequest request);
    }
}

