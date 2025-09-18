using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.DTOs.Domain.Data;
using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Application.DTOs.Domain.Response;
using HyBrCRM.Application.DTOs.Verticals.Data;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Application.DTOs.Verticals.Response;
using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;

namespace HyBrCRM.Application.Interfaces.UserInterfaces
{
    public interface IDomainRepository 
    {
        Task<BaseResult<string>> CreateDomain(DomainRequest model);
        Task<BaseResult> DeleteDomain(string id);
        Task<BaseResult> UpdateDomain(string id, DomainRequest model);

        Task<BaseResult<DomainData>> GetDomainById(string id);
        Task<IReadOnlyList<DomainResponse>> GetAllAsync();

        Task<BaseResult<DomainResponse>> GetAllDomain();

    }
}
