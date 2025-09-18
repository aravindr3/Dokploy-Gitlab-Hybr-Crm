using System;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }

        public EmployeeDto(EmployeeMaster employeeMaster, string branchName, string reportingName)
        {
            Id = employeeMaster.Id;
            EmployeeCode = employeeMaster.EmployeeCode;
            Name = employeeMaster.Name;
            Mobile = employeeMaster.Mobile;
            Email = employeeMaster.Email;
            Address1 = employeeMaster.Address1;
            Address2 = employeeMaster.Address2;
            City = employeeMaster.City;
            Pin = employeeMaster.Pin;
            Phone = employeeMaster.Phone;
            DesignationId = employeeMaster.DesignationId;
            DesignationName = employeeMaster.Designation.CommonName;
            JoinDate = employeeMaster.JoinDate;
            ResignDate = employeeMaster.ResignDate;
            BranchId = employeeMaster.BranchId;
            BranchName = branchName ?? "Unknown";
            SpecifyBranch = employeeMaster.SpecifyBranch;
            ReportingTo = string.IsNullOrEmpty(employeeMaster.ReportingTo) ? null : employeeMaster.ReportingTo;
            ReportingToName = reportingName ?? "Unknown";
            Status = employeeMaster.Status;
        }

        public string? Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Name { get; set; }
        public string? Mobile { get; set; }

        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime ResignDate { get; set; }
        public string? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string ? SpecifyBranch {  get; set; }
        public string? ReportingTo { get; set; }
        public string? ReportingToName { get; set; }


        public int Status { get; set; }
    }
}