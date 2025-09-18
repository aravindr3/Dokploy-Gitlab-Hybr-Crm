using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.DeleteEmployeeCommand;

public class DeleteEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteEmployeeCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.Id);

        if (employee is null) return new Error(ErrorCode.NotFound, "Common employee is not found", nameof(request.Id));

        employee.Status = 0;
        employeeRepository.Update(employee);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}