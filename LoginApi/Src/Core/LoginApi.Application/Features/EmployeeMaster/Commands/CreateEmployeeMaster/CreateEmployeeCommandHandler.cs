using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.CreateEmployeeMaster;

public class CreateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IBranchServices branchServices,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateEmployeeMasterCommand, BaseResult<string>>
{
    public async Task<BaseResult<string>> Handle(CreateEmployeeMasterCommand request,
        CancellationToken cancellationToken)
    {
        var branchResult = await branchServices?.GetBranchbyId(request.BranchId);
        var reportingTo = await employeeRepository?.GetByIdAsync(request.ReportingTo);

        //if (branchResult == null || !branchResult.Success || branchResult.Data == null || reportingTo == null)
        //    return new Error(ErrorCode.NotFound, "Branch not found ");
        var branchId = branchResult?.Data?.Id;

        if (request.BranchId == "Other")
        {
            branchId = "Other";
        }
        var existingEmployees = await employeeRepository.GetAllAsync();
        if (existingEmployees.Any(e => e.EmployeeCode == request.EmployeeCode))
        {
            //return new BaseResult<string> { Success = false};
            return new Error(ErrorCode.NotFound, "EmployeeCode is already existed");
        }

        var employee = new Domain.Exchange.Entities.EmployeeMaster(
            request.EmployeeCode,
            request.Name,
            request.Mobile,
            request.Email,
            request.Address1,
            request.Address2,
            request.City,
            request.Pin,
            request.Phone,
            request.DesignationId = string.IsNullOrEmpty(request.DesignationId) ? null : request.DesignationId,
            request.JoinDate,
            request.ResignDate,
            request.ReportingTo = string.IsNullOrEmpty(request.ReportingTo) ? null : request.ReportingTo,
            request.SpecifyBranch
        );

        employee.Id = Ulid.NewUlid().ToString();
        employee.BranchId = branchId;

        await employeeRepository.AddAsync(employee);
        await unitOfWork.SaveChangesAsync();

        return new BaseResult<string> { Success = true, Data = employee.Id };
    }
}