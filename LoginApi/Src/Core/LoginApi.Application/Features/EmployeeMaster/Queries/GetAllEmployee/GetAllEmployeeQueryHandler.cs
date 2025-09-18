using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Queries.GetAllEmployee;

public class GetAllEmployeeQueryHandler(
    IEmployeeRepository employeeRepository,
    IBranchServices branchServices) : IRequestHandler<GetAllEmployeeQuery, BaseResult<List<EmployeeDto>>>
{
    public async Task<BaseResult<List<EmployeeDto>>> Handle(GetAllEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await employeeRepository.GetAllWithChildAsync(o => o.Designation);

        if (employees == null || !employees.Any()) return new BaseResult<List<EmployeeDto>> { Success = false };

        var branchDictionary = new Dictionary<string, string>();
        var employeeDictionary = employees.ToDictionary(e => e.Id, e => e.Name);
        var dtoList = new List<EmployeeDto>();

        foreach (var employee in employees.Where(a => a.Status == 1))
        {
            var branchName = "Other";
            if (employee.BranchId == "Other")
            {
                branchName = "Other";
            }
            else
            {
                if (!string.IsNullOrEmpty(employee.BranchId))
                    if (!branchDictionary.TryGetValue(employee.BranchId, out branchName))
                    {
                        var branchResult = await branchServices.GetBranchbyId(employee.BranchId);
                        if (branchResult.Success && branchResult.Data != null)
                        {
                            branchName = branchResult?.Data?.BranchName ?? "Other";
                            branchDictionary[employee.BranchId] = branchName;
                        }
                    }
            }

            var reportingToName = "Not Assigned";
            if (!string.IsNullOrEmpty(employee.ReportingTo) &&
                employeeDictionary.TryGetValue(employee.ReportingTo, out var empName)) reportingToName = empName;

            dtoList.Add(new EmployeeDto
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                Name = employee.Name,
                Mobile = employee.Mobile,
                Email = employee.Email,
                Address1 = employee.Address1,
                Address2 = employee.Address2,
                City = employee.City,
                Pin = employee.Pin,
                Phone = employee.Phone,
                DesignationId = employee.DesignationId,
                DesignationName = employee.Designation?.CommonName ?? "Unknown",
                JoinDate = employee.JoinDate,
                ResignDate = employee.ResignDate,
                BranchId = employee.BranchId,
                BranchName = branchName,
                SpecifyBranch = employee.SpecifyBranch,
                ReportingTo = employee.ReportingTo ?? "default",
                ReportingToName = reportingToName ?? "default",
                Status = employee.Status
            });
        }

        return BaseResult<List<EmployeeDto>>.Ok(dtoList);
    }
}