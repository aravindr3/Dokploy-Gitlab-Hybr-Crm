using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.HolidaysLead.Command.Create;
using HyBrCRM.Application.Features.Lead.Command.Delete;
using HyBrCRM.Application.Features.HolidaysLead.Command.Delete;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Features.HolidaysLead.Command.Update;
using HyBrCRM.Application.Features.Lead.Query.GetById;
using HyBrCRM.Application.Features.HolidaysLead.Query.GetById;
using HyBrCRM.Application.Features.Lead.Query.GetAll;
using HyBrCRM.Application.Features.HolidaysLead.Query.GetAll;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class HolidayLeadcontrollers : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateHolidayLead([FromBody] CreateHolidayLeadCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteHolidayLead([FromQuery] DeleteHolidayLeadCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPost]
        public async Task<BaseResult> UpdateHolidayLead([FromBody] UpdateHolidayLeadCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult> GetHolidayLead([FromQuery] GetHolidayleadById model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult> GetAllHolidayLead([FromQuery] GetAllHolidayleadQuery model)
        {
            return await Mediator.Send(model);
        }

    }
}
