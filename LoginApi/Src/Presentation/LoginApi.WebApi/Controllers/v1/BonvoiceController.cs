using HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;
using HyBrForex.Application.Wrappers;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HyBrCRM.Application.Features.Bonvoice.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Domain.Bonvoice.DTO.Response;
using HyBrCRM.Domain.Exchange.DTOs;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class BonvoiceController(IBonVoiceCallServices bonVoiceCallServices) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateBonvoice([FromBody] CreateBonvoiceCommand model)
        {
            return await Mediator.Send(model);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<BaseResult<AuthResponse>>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await bonVoiceCallServices.AuthenticateBonvoiceAsync(request);
            return Ok(result);
        }
        [HttpPost("bridge-call")]
        public async Task<ActionResult<BaseResult<string>>> BridgeCall([FromBody] BonvoiceBridgeRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await bonVoiceCallServices.AutoCallBridgeAsync(dto.AuthRequest, dto.CallRequest);
            return Ok(result);
        }
    }
}
