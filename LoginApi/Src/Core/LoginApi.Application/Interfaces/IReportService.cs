using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.Report.Request;
using HyBrForex.Application.DTOs.Report.Response;
using HyBrForex.Application.DTOs.Role.Requests;
using HyBrForex.Application.DTOs.Role.Responses;
using HyBrForex.Application.Wrappers;
using LoginApi.Application.Interfaces;

namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IReportService
    {
        Task<BaseResult<ReportResponse>> CreateReportAsync(ReportRequest request);
        Task<BaseResult<ReportResponse>> UpdateReportAsync(string id, ReportRequest request);
        Task<BaseResult<bool>> DeleteReportAsync(string id);
        Task<BaseResult<ReportResponse>> GetByIdAsync(string id);
        Task<BaseResult<IEnumerable<ReportResponse>>> GetAllAsync();
        Task<BaseResult<ReportFeatureResponse>> CreateAsync(ReportFeatureRequest request);
        Task<BaseResult<ReportFeatureResponse>> UpdateAsync(string id, ReportFeatureRequest request);
        Task<BaseResult<bool>> DeleteAsync(string id);
        Task<BaseResult<List<ReportFeatureResponse>>> GetAllFeatureAsync();
        Task<BaseResult<ReportFeatureResponse>> GetByIdFeatureAsync(string id);
        Task<BaseResult<ReportRoleResponse>> GetFeaturePermissionsByRoleIdAsync(string roleId);
        Task<BaseResult<ReportRoleResponse>> GetFeaturePermissionsByRoleIdCategoryAsync(string roleId, string category);

        Task<BaseResult<string>> CreateFeaturePermissionsAsync(ReportRoleMappingRequest request);

        Task<BaseResult<RoleTyprReportResponse>> GetReportPermissionsByRoleIdAsync(string roleId);
        Task<BaseResult<ReportQueryResponse>> ExecuteReportAsync(ReportQueryRequest request);


    }
}
