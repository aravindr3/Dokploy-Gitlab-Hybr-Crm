using HyBrCRM.Application.Features.LeadProperties.Queries.GetById;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HyBrCRM.Application.Features.ActivityLog.Query.GetByLeadId;
using HyBrCRM.Application.Features.ActivityLog.Query.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class ActivityLogController : BaseApiController
    {
        [HttpGet]
        public async Task<BaseResult<List<ActivityLogDto>>> GetActivityByLead([FromQuery] GetActivityLogByLeadIdQuery model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet]
        public async Task<BaseResult<List<ActivityLogDto>>> GetAllActivityLog([FromQuery] GetAllActivityLog model)
        {
            return await Mediator.Send(model);
        }

    }
}
