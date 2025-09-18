using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Tenant.Requests;
using HyBrForex.Application.DTOs.Tenant.Responses;
using HyBrForex.Application.Wrappers;



namespace HyBrForex.Application.Interfaces.UserInterfaces
{
    public interface ITenantService
    {
        Task<BaseResult<TenantResponse>> CreateAsync(CreateRequest request);
        Task<BaseResult<TenantResponse>> UpdateAsync(UpdateRequest request);
        Task<BaseResult<bool>> DeleteAsync(string id);
        Task<BaseResult<GetApplicationTenantResponse>> GetById(String id);
        Task<BaseResult<IEnumerable<GetApplicationTenantResponse>>> GetAllAsync();
        Task<BaseResult<IEnumerable<GetApplicationResponseGS>>> GetAllAsyncGS();
    }
}
