using HyBrForex.Application.DTOs.Branch.Request;
using HyBrForex.Application.Wrappers;
using HyBrForex.Infrastructure.Identity.Services;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Infrastructure.Identity.Services;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Application.DTOs.Verticals.Response;
using HyBrCRM.Application.DTOs.Domain.Response;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class DomainController(IDomainRepository domainRepository) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateDomain(DomainRequest model)
        {
            return await domainRepository.CreateDomain(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteDomain(string id)
        {
            return await domainRepository.DeleteDomain(id);
        }

        [HttpPost]
        public async Task<BaseResult> UpdateDomain(string id, DomainRequest model)
        {
            return await domainRepository.UpdateDomain(id, model);
        }

        [HttpGet]
        public async Task<BaseResult> GetDomainById(string id)
        {
            return await domainRepository.GetDomainById(id);
        }

        [HttpGet]

        public async Task<BaseResult<DomainResponse>> GetAllDomain()
        {
            return await domainRepository.GetAllDomain();
        }
    }
}
