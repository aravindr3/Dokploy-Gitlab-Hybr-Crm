using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler(
    IEmployeeRepository employeeRepository,   
    IBranchServices branchServices) : IRequestHandler<GetEmployeeByIdQuery, BaseResult<EmployeeDto>>
{
    public async Task<BaseResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.GetByIdChildAsync(
            a => a.Id == request.Id,
            b => b.Designation
        );

        var employee = employees.FirstOrDefault();

        if (employee is null || employee.Status == 0)
            return new Error(ErrorCode.NotFound, "Employee is not found", nameof(request.Id));


        var branchResult = await branchServices.GetBranchbyId(employee.BranchId);
        var branchName = branchResult.Success && branchResult.Data != null
            ? branchResult?.Data?.BranchName
            : "Other";


        var reportingToName = "Not Assigned";
        if (!string.IsNullOrEmpty(employee.ReportingTo))
        {
            var reportingEmployee = await employeeRepository.GetByIdAsync(employee.ReportingTo);
            if (reportingEmployee != null) reportingToName = reportingEmployee.Name;
        }

        var employeeDto = new EmployeeDto(employee, branchName, reportingToName);

        return employeeDto;
    }
}