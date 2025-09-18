using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Application.Wrappers;


namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface IFunctionalMasterService
    {
        Task<BaseResult<FunctionalMasterResponse>> CreateAsync(FunctionalMasterRequest request);
        Task<BaseResult<FunctionalMasterResponse>> UpdateAsync(String id, FunctionalMasterRequest request);
        Task<BaseResult<bool>> DeleteAsync(string id);
        Task<BaseResult<List<FunctionalMasterResponse>>> GetAllAsync();
        Task<BaseResult<FunctionalMasterResponse>> GetByIdAsync(string id);
        Task<BaseResult<string>> CreateFeaturePermissionsAsync(FeatureRoleMappingRequest request);
        Task<BaseResult<FeatureRoleResponse>> GetFeaturePermissionsByRoleIdAsync(string roleId);
        Task<BaseResult<List<FunctionalMasterResponse>>> GetAllNewAsync(List<string> roleIds);
        Task<BaseResult<GetRouterLinksResponse>> GetRouterLinksByRoleAsync(List<string> roleIds);

    }

}
