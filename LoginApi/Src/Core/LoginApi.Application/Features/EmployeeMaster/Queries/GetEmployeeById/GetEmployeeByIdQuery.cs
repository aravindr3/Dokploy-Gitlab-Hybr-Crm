using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<BaseResult<EmployeeDto>>
{
    public string Id { get; set; }
}