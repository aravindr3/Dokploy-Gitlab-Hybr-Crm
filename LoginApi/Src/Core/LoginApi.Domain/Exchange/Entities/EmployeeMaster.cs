using System;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class EmployeeMaster : AuditableBaseEntity
    {


        public EmployeeMaster()
        {
        }

        public EmployeeMaster(string employeeCode, string name, string mobile, string email, string address1,
            string address2, string city, string pin, string phone, string designation, DateTime joinDate,
            DateTime resignDate, string reportingTo , string ? specifyBranch)
        {
            EmployeeCode = employeeCode;
            Name = name;
            Mobile = mobile;
            Email = email;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Pin = pin;
            Phone = phone;
            DesignationId = designation;
            JoinDate = joinDate;
            ResignDate = resignDate;
            ReportingTo = reportingTo;
            SpecifyBranch = specifyBranch;
        }


        public string? EmployeeCode { get; set; }
        public string? Name { get; set; }
        public string Mobile { get; set; }

        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string Phone { get; set; }
        public string? DesignationId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime ResignDate { get; set; }
        public string? BranchId { get; set; }
        public string ? SpecifyBranch {  get; set; }
        public string? ReportingTo { get; set; }

        public virtual CommonMsater? Designation { get; set; }

        public void Update(string employeeCode, string name, string mobile, string email, string address1,
            string address2,
            string city, string pin, string phone, string designation, DateTime joinDate, DateTime resignDate,
            string reportingTo , string ? specifyBranch)
        {
            EmployeeCode = employeeCode;
            Name = name;
            Mobile = mobile;
            Email = email;
            Address1 = address1;
            Address2 = address2;
            City = city;
            Pin = pin;
            Phone = phone;
            DesignationId = designation;
            JoinDate = joinDate;
            ResignDate = resignDate;
            ReportingTo = reportingTo;
            SpecifyBranch = specifyBranch;
        }
    }
}