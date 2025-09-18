using HyBrCRM.Application.Features.LeadContact.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrCRM.Application.Features.LeadContact.Command.Delete;
using HyBrCRM.Application.Features.LeadContact.Command.Update;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Features.Lead.Command.Delete;
using HyBrCRM.Application.Features.LeadContact.Queries.GetById;
using HyBrCRM.Application.Features.Lead.Query.GetById;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrCRM.Application.Features.Lead.Query.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion ("1")]
    public class LeadController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult> CreateLead([FromBody] CreateLeadCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpPost]
        public async Task<BaseResult> UpdateLead([FromBody] UpdateLeadCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteLead([FromQuery] DeleteLeadCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult> GetLead([FromQuery] GetLeadByIdQuery model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult> GetAllLead([FromQuery] GetAllLeadQuery model)
        {
            return await Mediator.Send(model);
        }

    }
}
