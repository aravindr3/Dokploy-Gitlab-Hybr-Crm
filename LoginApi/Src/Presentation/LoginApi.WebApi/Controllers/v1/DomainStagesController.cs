using HyBrCRM.Application.DTOs.Domain.Request;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.DTOs.Verticals.Request;
using HyBrCRM.Infrastructure.Identity.Services;
using HyBrCRM.Application.Features.TaskMaster.Command.Create;
using HyBrCRM.Application.Features.DomainStages.Command.Create;
using HyBrForex.Application.Features.StateMaster.Queries;
using HyBrForex.Domain.Exchange.DTOs;
using System.Collections.Generic;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Application.Features.DomainStages.Queries.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class DomainStagesController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult> CreateDomainStages([FromBody] CreateDomainStagesCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<DomainStagesDto>>> GetAllDomainStages([FromQuery] GetAllDomainStages model)
        {
            return await Mediator.Send(model);
        }
    }
}
