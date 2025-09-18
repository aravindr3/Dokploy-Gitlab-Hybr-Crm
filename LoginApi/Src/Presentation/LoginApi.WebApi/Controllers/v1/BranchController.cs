using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace HyBrForex.WebApi.Controllers.v1;

[ApiVersion("1")]
public class BranchController(IBranchServices branchServices) : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult<string>> CreateBranch(BranchRequest model)
    {
        return await branchServices.CreateBranch(model);
    }

    [HttpDelete]
    public async Task<BaseResult> DeleteBranch(string id)
    {
        return await branchServices.DeleteBranch(id);
    }

    [HttpPut]
    public async Task<BaseResult> UpdateBranch(string id, BranchRequest model)
    {
        return await branchServices.UpdateBranch(id, model);
    }

    [HttpGet]
    public async Task<BaseResult> GetBranchbyId(string id)
    {
        return await branchServices.GetBranchbyId(id);
    }

    [HttpGet]
    public async Task<BaseResult<BranchResponse>> GetAllBranch()
    {
        return await branchServices.GetAllBranch();
    }

    [HttpGet]
    public async Task<BaseResult<BranchResponse>> GetAllBrachByTenantId( string TenantId)
    {
        return await branchServices.GetAllBrachByTenantId(TenantId);
    }
}