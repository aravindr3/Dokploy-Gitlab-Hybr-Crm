using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Queries.GetAllEmployee;

public class GetAllEmployeeQuery : IRequest<BaseResult<List<EmployeeDto>>>
{
}