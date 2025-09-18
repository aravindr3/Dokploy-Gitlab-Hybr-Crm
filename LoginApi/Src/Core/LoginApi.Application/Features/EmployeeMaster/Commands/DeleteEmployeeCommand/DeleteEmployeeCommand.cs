using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.DeleteEmployeeCommand;

public class DeleteEmployeeCommand : IRequest<BaseResult>
{
    public string Id { get; set; }
}