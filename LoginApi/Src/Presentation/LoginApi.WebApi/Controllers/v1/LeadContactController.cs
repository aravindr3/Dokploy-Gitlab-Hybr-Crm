using HyBrForex.Application.Features.EmployeeMaster.Commands.CreateEmployeeMaster;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.LeadContact.Command.Create;
using HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;
using HyBrCRM.Application.Features.LeadContact.Command.Update;
using HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;
using HyBrCRM.Application.Features.LeadContact.Command.Delete;
using HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;
using HyBrCRM.Application.Features.LeadContact.Queries.GetById;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion ("1")]
    public class LeadContactController : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult> CreateLeadContact([FromBody] CreateLeadContactCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPost]
        public async Task<BaseResult> UpdateLeadContact([FromBody] UpdateLeadContactCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpDelete]
        public async Task<BaseResult> DeleteLeadContact([FromQuery] DeleteLeadContactCommand model)
        {
            return await Mediator.Send(model);
        }


        [HttpGet]
        public async Task<BaseResult> GetAllLeadContact([FromQuery] GetAllLeadContactQuery model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet]
        public async Task<BaseResult> GetLeadContactById([FromQuery] GetLeadCotactByIdQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
