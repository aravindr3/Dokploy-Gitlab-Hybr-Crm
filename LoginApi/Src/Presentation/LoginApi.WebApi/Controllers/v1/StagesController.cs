using HyBrForex.Application.Features.StateMaster.Command;
using HyBrForex.Application.Features.StateMaster.Queries;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.Stages.Command.Create;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Application.Features.Stages.Queries.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class StagesController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateStages([FromBody] CreateStagesCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<StagesDto>>> GetAllState([FromQuery] GetAllStagesQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
