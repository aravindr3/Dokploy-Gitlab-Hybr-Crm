using System;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<BaseResult>
{
    public string ID { get; set; }
    public string EmployeeCode { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }

    public string Email { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Pin { get; set; }
    public string Phone { get; set; }
    public string Designation { get; set; }
    public DateTime JoinDate { get; set; }
    public DateTime ResignDate { get; set; }
    public string BranchId { get; set; }
    public string ? SpecifyBranch {  get; set; }
    public string ReportingTo { get; set; }
}