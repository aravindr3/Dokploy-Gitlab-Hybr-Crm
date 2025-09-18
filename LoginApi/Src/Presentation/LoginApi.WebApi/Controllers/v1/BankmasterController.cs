
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;
using HyBrForex.Application.Features.Bankmaster.Commands.CreateBankmaster;

using HyBrForex.Domain.Exchange.DTOs;
using System.Collections.Generic;

using HyBrForex.Application.Features.Bankmaster.Queries.GetAllBanks;
using HyBrForex.Application.Features.Bankmaster.Commands.DeleteBankmaster;
using HyBrForex.Application.Features.Bankmaster.Commands.UpdateBankmaster;
using HyBrForex.Application.Features.BankDetail.Commands.CreateBankDetail;
using HyBrForex.Application.Features.BankDetail.Commands.DeleteBankDetail;
using HyBrForex.Application.Features.BankDetail.Commands.UpdateBankDetail;
using HyBrForex.Application.Features.BankDetail.Queries.GetAllBankDetails;

using HyBrForex.Application.Features.BankDetail.Queries.GetBankDetailsById;
using HyBrForex.Application.Features.Bankmaster.Queries.GetBankById;

namespace HyBrForex.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class BankmasterController(IBankmasterRepository bankmasterRepository) : BaseApiController
    {
        [HttpPost]
        public async Task<BaseResult<string>> CreateBankmaster([FromBody] CreateBankmasterCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<BankMasterDto>>> GetAllBankmaster([FromQuery] GetAllBanksQuery model)
        {
            return await Mediator.Send(model);
        }

        [HttpGet]
        public async Task<BaseResult<BankMasterDto>> GetBankById([FromQuery] GetBankByIdQuery model)
        {
            return await Mediator.Send(model);
        }
        [HttpDelete]
        public async Task<BaseResult> DeleteBankmaster([FromQuery] DeleteBankmasterCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPut]
        public async Task<BaseResult> UpdateBankmaster([FromBody] UpdateBankmasterCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPost]
        public async Task<BaseResult<string>> CreateBankDetails([FromBody] CreateBankDetailCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpDelete]
        public async Task<BaseResult> DeleteBankdetails([FromQuery] DeleteBankDetailCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpPut]
        public async Task<BaseResult> UpdateBankdetails([FromBody] UpdateBankDetailCommand model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<List<BankdetailsDto>>> GetAllBankDetails([FromQuery] GetAllBankDetailsQuery model)
        {
            return await Mediator.Send(model);
        }
        [HttpGet]
        public async Task<BaseResult<BankdetailsDto>> GetBankDetailsById([FromQuery] GetBankdetailsByIdQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
