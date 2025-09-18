using System;
using System.ComponentModel.DataAnnotations;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.EmployeeMaster.Commands.CreateEmployeeMaster;

public class CreateEmployeeMasterCommand : IRequest<BaseResult<string>>
{
    public string EmployeeCode { get; set; }

    [Required] public string Name { get; set; }

    public string Mobile { get; set; }

    [Required][EmailAddress] public string Email { get; set; }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Pin { get; set; }
    public string Phone { get; set; }
    public string DesignationId { get; set; }
    public DateTime JoinDate { get; set; }
    public DateTime ResignDate { get; set; }
    public string BranchId { get; set; }
    public string ? SpecifyBranch {  get; set; }
    public string ReportingTo { get; set; }
}