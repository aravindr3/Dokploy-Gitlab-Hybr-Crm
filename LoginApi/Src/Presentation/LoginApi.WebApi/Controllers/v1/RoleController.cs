using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.Interfaces.UserInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.DTOs.Role.Responses;
using System.Collections.Generic;
using System.Linq;
using LoginApi.WebApi.Controllers;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class RoleController(IRoleService roleService) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<RoleResponse>> Create([FromBody] RoleRequest request)
        {
            return await roleService.CreateRoleAsync(request);
        }

        [HttpPost("{id}")]
        public async Task<BaseResult<RoleResponse>> Update(string id, [FromBody] RoleRequest request)
        {
            return await roleService.UpdateRoleAsync(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult> Delete(string id)
        {
            var result = await roleService.DeleteRoleAsync(id);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<BaseResult<RoleResponse>> GetById([FromRoute] string id)
        {
            var role = await roleService.GetByIdAsync(id);
            return role;
        }

        [HttpGet]
        public async Task<BaseResult<IEnumerable<RoleResponse>>> GetAll()
        {
            var roles = (await roleService.GetAllAsync());
            return roles;
        }

    }


}
