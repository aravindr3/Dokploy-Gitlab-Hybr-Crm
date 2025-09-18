using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;
using HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;
using HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;
using HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;
using HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;
using HyBrForex.Application.Wrappers;
using HyBrForex.WebApi.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HyBrForex.WebApi.Controllers.v1;

//[ServiceFilter(typeof(AuthenticationFilter))]
[ApiVersion("1")]
public class CommonMasterController : BaseApiController
{
    [HttpPost]
    public async Task<UlidBaseResult<string>> CreateCommonMaster([FromBody] CreateCommonMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpDelete]
    public async Task<BaseResult> DeleteCommonMaster([FromQuery] DeleteCommonMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpPut]
    public async Task<BaseResult> UpdateCommonMaster([FromBody] UpdateCommonMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult> GetCommonMasterById([FromQuery] GetCommonMasterByIdQuery model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult> GetAllCommonMaster([FromQuery] GetAllCommonMasterQuery model)
    {
        return await Mediator.Send(model);
    }
}