using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonTypeMasters.Commands.CreateCommonTypeMaster;
using HyBrForex.Application.Features.CommonTypeMasters.Commands.DeleteCommonTypeMsater;
using HyBrForex.Application.Features.CommonTypeMasters.Commands.UpdateCommonTypeMaster;
using HyBrForex.Application.Features.CommonTypeMasters.Queries.GetAllCommonTypeMasterQuery;
using HyBrForex.Application.Features.CommonTypeMasters.Queries.GetCommonTypeMasterById;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using Microsoft.AspNetCore.Mvc;

//using HyBrForex.Application.Features.Products.Commands.UpdateProduct;
//using LoginApi.Application.Features.Products.Queries.GetProductById;

namespace HyBrForex.WebApi.Controllers.v1;

//[ServiceFilter(typeof(AuthenticationFilter))]
[ApiVersion("1")]
public class CommonTypeMasterController : BaseApiController
{
    [HttpPost]
    public async Task<UlidBaseResult<string>> CreateCommonTypeMaster(CreateCommonTypeMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpDelete]
    public async Task<BaseResult> DeleteCommonTypeMaster([FromQuery] DeleteCommonTypeMsater model)
    {
        return await Mediator.Send(model);
    }

    [HttpPut]
    public async Task<BaseResult> UpdateCommonTypeMaster(UpdateCommonTypeMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult<CommonTypeMsaterDto>> GetCommonTypeMasterById(
        [FromQuery] GetCommonTypeMasterByIdQuery model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult<List<CommonTypeMsaterDto>>> GetAllCommonTypeMasterQuery(
        [FromQuery] GetAllCommonTypeMasterQuery model)
    {
        return await Mediator.Send(model);
    }
}