using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.DTOs.Role.Responses;
using HyBrForex.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginApi.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.DTOs.Report.Response;
using HyBrForex.Application.DTOs.Report.Request;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Infrastructure.Identity.Services;
using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class ReportController(IReportService reportService) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<ReportResponse>> Create([FromBody] ReportRequest request)
        {
            return await reportService.CreateReportAsync(request);
        }

        [HttpPost("{id}")]
        public async Task<BaseResult<ReportResponse>> Update(string id, [FromBody] ReportRequest request)
        {
            return await reportService.UpdateReportAsync(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult> Delete(string id)
        {
            var result = await reportService.DeleteReportAsync(id);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<BaseResult<ReportResponse>> GetById([FromRoute] string id)
        {
            var role = await reportService.GetByIdAsync(id);
            return role;
        }

        [HttpGet]
        public async Task<BaseResult<IEnumerable<ReportResponse>>> GetAll()
        {
            var roles = (await reportService.GetAllAsync());
            return roles;
        }
        [HttpPost]
        public async Task<BaseResult<ReportFeatureResponse>> CreateFeature([FromBody] ReportFeatureRequest request)
        {
            var result = await reportService.CreateAsync(request);
            return (result);
        }

        [HttpPost("{id}")]
        public async Task<BaseResult<ReportFeatureResponse>> UpdateFeatureAsync(string id, [FromBody] ReportFeatureRequest request)
        {
            var result = await reportService.UpdateAsync(id, request);
            return (result);
        }

        [HttpDelete("{id}")]
        public async Task<BaseResult<bool>> DeleteFeature(string id)
        {
            var Sucess = await reportService.DeleteAsync(id);
            return Sucess;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeature()
        {
            var result = await reportService.GetAllFeatureAsync();
            if (result.Success)
                return Ok(result);
            return NotFound(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFeature(string id)
        {
            var result = await reportService.GetByIdFeatureAsync(id);
            if (result.Success)
                return Ok(result);

            return NotFound(result);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetFeaturePermissions(string roleId)
        {
            var result = await reportService.GetFeaturePermissionsByRoleIdAsync(roleId);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetCategoryPermissions(string roleId, [FromQuery] string category)
        {
            var result = await reportService.GetFeaturePermissionsByRoleIdCategoryAsync(roleId, category);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPost]
        public async Task<BaseResult<string>> CreateFeaturePermissionsAsync([FromBody] ReportRoleMappingRequest request)
        {
            var result = await reportService.CreateFeaturePermissionsAsync(request);
            return (result);
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetReportPermissions(string roleId)
        {
            var result = await reportService.GetReportPermissionsByRoleIdAsync(roleId);
            return result.Success ? Ok(result) : NotFound(result);
        }
        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteReport([FromBody] ReportQueryRequest request)
        {
            
                var result = await reportService.ExecuteReportAsync(request);
                return result.Success ? Ok(result) : NotFound(result);
        }
    }
}

