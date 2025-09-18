using HyBrForex.Application.DTOs.User.Requests;
using System.Threading.Tasks;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.Wrappers;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.DTOs.Tenant.Responses;
using System.Collections.Generic;
using HyBrForex.Application.DTOs.User.Responses;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class UserController(IUserService userService) : BaseApiController
    {
        [HttpPost("create")]
        public async Task<BaseResult<IActionResult>> CreateUser([FromBody] UserRequest request)
        {
            var response = await userService.CreateUserAsync(request);
            return Ok(response);
        }

        [HttpPost("update/{userId}")]
        public async Task<BaseResult<IActionResult>> UpdateUser(string userId, [FromBody] UserRequest request)
        {
            var response = await userService.UpdateUserAsync(userId, request);
            return Ok(response);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<BaseResult<IActionResult>> DeleteUser(string userId)
        {
            var success = await userService.DeleteUserAsync(userId);
            return Ok(success);
            
        }
        [HttpPost("getAll")]
        public async Task<BaseResult<IEnumerable<GetUserResponse>>> GetAll()
        {
            var users = await userService.GetAllUsersAsync();
            return users;
        }

        [HttpGet("getById/{id}")]
        public async Task<BaseResult<GetUserResponse>> GetById(string id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return BaseResult<GetUserResponse>.Failure(new Error(ErrorCode.NotFound, "User not found"));

            }


            return user;
        }
        [HttpPost]
        public async Task<BaseResult<List<GetBranchByTenantResponse>>> GetBranchByTenantAsync(GetBranchByTenantRequest request)
        {
            var users = await userService.GetBranchByTenantAsync(request);

            return users;
        }
    }
}
