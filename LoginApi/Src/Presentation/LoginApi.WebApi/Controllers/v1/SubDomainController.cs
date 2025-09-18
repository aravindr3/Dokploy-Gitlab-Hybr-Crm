using HyBrCRM.Application.Features.DomainStages.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.SubDomain.Command.Create;
using HyBrCRM.Application.Features.DomainStages.Queries.GetAll;
using HyBrCRM.Domain.Exchange.DTOs;
using System.Collections.Generic;
using HyBrCRM.Application.Features.SubDomain.Queries.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class SubDomainController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateSubDomain([FromBody] CreateSubDomainCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<SubDomainDto>>> GetAllSubDomain([FromQuery] GetAllSubDomainquery model)
        {
            return await Mediator.Send(model);
        }
    }
}
