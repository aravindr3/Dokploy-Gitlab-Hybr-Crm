using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Branch.Data;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.DTOs.Branch.Response;
using HyBrForex.Application.Wrappers;

namespace HyBrForex.Application.Interfaces.UserInterfaces;

public interface IBranchServices
{
    Task<BaseResult<string>> CreateBranch(BranchRequest model);

    Task<BaseResult> UpdateBranch(string id, BranchRequest model);

    Task<BaseResult> DeleteBranch(string id);

    Task<BaseResult<BranchData>> GetBranchbyId(string id);

    Task<IReadOnlyList<BranchResponse>> GetAllAsync();

    Task<BaseResult<BranchResponse>> GetAllBranch();
    Task<BaseResult<BranchResponse>> GetAllBrachByTenantId(string TenantId);

}