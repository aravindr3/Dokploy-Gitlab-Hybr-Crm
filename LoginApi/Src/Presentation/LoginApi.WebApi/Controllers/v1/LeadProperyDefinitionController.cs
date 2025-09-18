using HyBrCRM.Application.Features.LeadProperties.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.LeadProperyDefinition.Create;
using HyBrCRM.Application.Features.LeadProperties.Queries.GetAll;
using HyBrCRM.Domain.Exchange.DTOs;
using System.Collections.Generic;
using HyBrCRM.Application.Features.LeadProperyDefinition.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class LeadProperyDefinitionController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateLeadProperyDefinition([FromBody] CreateLeadProperyDefinitionCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet]
        public async Task<BaseResult<List<LeadPropertyDefinitionDto>>> GetAllLeadPropertyDefinition([FromQuery] GetAllLeadProperyDefinitionQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
