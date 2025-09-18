using System.Collections.Generic;
using System.Threading.Tasks;
using HyBrForex.Application.Features.EmployeeMaster.Commands.CreateEmployeeMaster;
using HyBrForex.Application.Features.EmployeeMaster.Commands.DeleteEmployeeCommand;
using HyBrForex.Application.Features.EmployeeMaster.Commands.UpdateEmployee;
using HyBrForex.Application.Features.EmployeeMaster.Queries.GetAllEmployee;
using HyBrForex.Application.Features.EmployeeMaster.Queries.GetEmployeeById;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HyBrForex.WebApi.Controllers.v1;

[ApiVersion("1")]
//[ServiceFilter(typeof(AuthenticationFilter))]
public class EmployeeMasterController : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult> CreateEmployee([FromBody] CreateEmployeeMasterCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpDelete]
    public async Task<BaseResult> DeleteEmployee([FromBody] DeleteEmployeeCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpPut]
    public async Task<BaseResult> UpdateEmployee([FromBody] UpdateEmployeeCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult<EmployeeDto>> GetEmployeeById([FromQuery] GetEmployeeByIdQuery model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult<List<EmployeeDto>>> GetAllEmployee([FromQuery] GetAllEmployeeQuery model)
    {
        return await Mediator.Send(model);
    }
}