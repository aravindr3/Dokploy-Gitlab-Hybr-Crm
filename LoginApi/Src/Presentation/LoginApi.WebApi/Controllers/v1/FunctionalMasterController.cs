using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using System.Threading.Tasks;
using System;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.DTOs.Feature.Request;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]

    public class FunctionalMasterController(IFunctionalMasterService functionalMasterService) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<FunctionalMasterResponse>> Create([FromBody] FunctionalMasterRequest request)
        {
            var result = await functionalMasterService.CreateAsync(request);
            return (result);
        }

        [HttpPost]
        public async Task<BaseResult<string>> CreateFeaturePermissionsAsync([FromBody] FeatureRoleMappingRequest request)
        {
            var result = await functionalMasterService.CreateFeaturePermissionsAsync(request);
            return (result);
        }

        [HttpPost("{id}")]
        public async Task<BaseResult<FunctionalMasterResponse>> Update(string id, [FromBody] FunctionalMasterRequest request)
        {
            var result = await functionalMasterService.UpdateAsync(id, request);
            return (result);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult<bool>> Delete(string id)
        {
            var Sucess = await functionalMasterService.DeleteAsync(id);
            return Sucess;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await functionalMasterService.GetAllAsync();
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetroleIds(RoleFunctionRequest request)
        {
            var result = await functionalMasterService.GetAllNewAsync(request.RoleIds);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await functionalMasterService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }
      

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetFeaturePermissions(string roleId)
        {
            var result = await functionalMasterService.GetFeaturePermissionsByRoleIdAsync(roleId);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetRouterLinksByRoles(RoleFunctionRequest request)
        {
            var result = await functionalMasterService.GetRouterLinksByRoleAsync(request.RoleIds);
            if (result.Success)
                return Ok(result);

            return NotFound(result);

        }
    }
}
