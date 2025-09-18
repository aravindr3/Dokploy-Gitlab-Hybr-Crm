using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.LeadProperties.Command.Create;
using HyBrCRM.Application.Features.Lead.Command.Delete;
using HyBrCRM.Application.Features.LeadProperties.Command.Delete;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Features.Lead.Query.GetById;
using HyBrCRM.Application.Features.LeadProperties.Queries.GetById;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Application.Features.Lead.Query.GetAll;
using System.Collections.Generic;
using HyBrCRM.Application.Features.LeadProperties.Queries.GetAll;
using HyBrCRM.Application.Features.LeadProperties.Command.Update;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion ("1")]
    public class LeadPropertiesController : BaseApiController
    {
        [HttpPost] 
        public async Task<BaseResult<string>> CreateLeadProperties([FromBody] CreateLeadPropertiesCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteLeadPropery([FromQuery] DeleteleadPropertiesCommand model)
        {
            return await Mediator.Send(model);
        }


        [HttpPost]
        public async Task<BaseResult> UpdateLeadProperties([FromBody] UpdateLeadProperties model)
        {
            return await Mediator.Send(model);
        }

        //[HttpGet]
        //public async Task<BaseResult<LeadPropertiesDto>> GetLeadPropertiesById([FromQuery] LeadPropertiesGetByIdQuery model)
        //{
        //    return await Mediator.Send(model);
        //}

        [HttpGet]
        public async Task<BaseResult<List<LeadPropertyValueDto>>> GetAllLeadProperties([FromQuery] GetAllLeadPropertiesquery model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<LeadPropertyValueDto>>> GetLeadPropertiesById([FromQuery] LeadPropertiesGetByIdQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
