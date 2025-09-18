using HyBrForex.Application.Features.Currency.Queries.GetAllcountrydtl;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HyBrCRM.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class CurrencyMasterController : BaseApiController
    {
        [HttpGet]
        public async Task<BaseResult<List<CountryMasterDto>>> GetAllcountrydtl([FromQuery] GetAllCountrydtlQuery model)
        {
            return await Mediator.Send(model);
        }
    }
}
