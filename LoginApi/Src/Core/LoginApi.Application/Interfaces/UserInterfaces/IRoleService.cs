using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.DTOs.Role.Responses;
using HyBrForex.Application.Wrappers;


namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IRoleService
    {
        Task<BaseResult<RoleResponse>> CreateRoleAsync(RoleRequest request);
        Task<BaseResult<RoleResponse>> UpdateRoleAsync(string id, RoleRequest request);
        Task<BaseResult<bool>> DeleteRoleAsync(string id);
        Task<BaseResult<RoleResponse>> GetByIdAsync(string id);
        Task<BaseResult<IEnumerable<RoleResponse>>> GetAllAsync();

    }
}
