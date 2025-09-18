using System.Threading.Tasks;
using System;
using HyBrForex.Application.Interfaces.UserInterfaces;
using LoginApi.Application.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Application.DTOs.Tenant.Requests;
using HyBrForex.Application.Wrappers;
using System.Collections.Generic;
using HyBrForex.Application.DTOs.Tenant.Responses;
using System.Linq;
using LoginApi.WebApi.Controllers;


namespace HyBrForex.WebApi.Controllers.v1
{
   
    [ApiVersion("1")]
    public class TenantController(ITenantService tenantService) : BaseApiController
    {
        [HttpPost("create")]
        public async Task<BaseResult<TenantResponse>> Create([FromBody] CreateRequest request)
        {
            if (!ModelState.IsValid) return BaseResult<TenantResponse>.Failure(new Error(ErrorCode.AlreadyExists,"Invalid request"));

            var result = await tenantService.CreateAsync(request);
            return result;
        }

        [HttpPost("update")]
        public async Task<BaseResult<TenantResponse>> Update([FromBody] UpdateRequest request)
        {
            if (!ModelState.IsValid) return BaseResult<TenantResponse>.Failure(new Error(ErrorCode.AlreadyExists,"Invalid request"));

            var result = await tenantService.UpdateAsync(request);
            return result;
        }
        [HttpDelete("delete/{id}")]
        public async Task<BaseResult> Delete(string id)
        {
            var success = await tenantService.DeleteAsync(id);

            return success;
        }


        [HttpGet("{id}")]
        public async Task<BaseResult<GetApplicationTenantResponse>> GetById(string id)
        {
            var tenant = await tenantService.GetById(id);
            if (tenant == null)
            {
                return BaseResult<GetApplicationTenantResponse>.Failure(new Error(ErrorCode.NotFound,"Tenant not found"));
            }
            return tenant;
        }

        [HttpGet]
        public async Task<BaseResult<IEnumerable<GetApplicationTenantResponse>>> GetAll()
        {
            var tenants = (await tenantService.GetAllAsync());
            return tenants;
        }

        [HttpGet]
        public async Task<BaseResult<IEnumerable<GetApplicationResponseGS>>> GetAllGS()
        {
            var tenants = (await tenantService.GetAllAsyncGS());
            return tenants;
        }
    }

}
