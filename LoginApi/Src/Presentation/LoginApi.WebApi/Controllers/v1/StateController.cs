
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.Features.StateMaster.Command;
using HyBrForex.Domain.Exchange.DTOs;
using System.Collections.Generic;
using HyBrForex.Application.Features.StateMaster.Queries;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class StateController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateStateMaster([FromBody] CreateStateMastercommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<StateMasterDto>>> GetAllState([FromQuery] GetAllstateQuery model)
        {
            return await Mediator.Send(model);
        }

    }
}
