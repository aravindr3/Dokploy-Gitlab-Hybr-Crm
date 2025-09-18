using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork,
    IBranchServices branchServices,
    ICommonMasterRepository commonMasterRepository) : IRequestHandler<UpdateEmployeeCommand, BaseResult>

{
    public async Task<BaseResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.GetByIdAsync(request.ID);
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
       

        if (employee is null) return new Error(ErrorCode.NotFound, "Employee not found", nameof(request.ID));

        employee.Update
        (
            request.EmployeeCode,
            request.Name,
            request.Mobile,
            request.Email,
            request.Address1,
            request.Address2,
            request.City,
            request.Pin,
            request.Phone,
             request.Designation = string.IsNullOrEmpty(request.Designation) ? null : request.Designation,
            request.JoinDate,
            request.ResignDate,
            request.ReportingTo = string.IsNullOrEmpty(request.ReportingTo) ? null : request.ReportingTo,
            request.SpecifyBranch
        );
        employee.BranchId = branchId;

        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}