using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class BankdetailsDto
    {
        public BankdetailsDto() { }
        public BankdetailsDto(Bankdetails bankdetails)
        {
            Id = bankdetails.Id;
            BankId = bankdetails.BankId;
            BankName = bankdetails.Bank.BankName;
            IFSCCode = bankdetails.IFSCCode;
            Branch = bankdetails.Branch;
            Address = bankdetails.Address;
            City1 = bankdetails.City1;
            City2 = bankdetails.City2;
            StateId = bankdetails.StateId;
            StateName = bankdetails.State.StateName;
            STDCode= bankdetails.STDCode;
            PhNumber = bankdetails.PhNumber;
            Status = bankdetails.Status;

        }
        public string? Id { get; set; }
        public string? BankId { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? Branch { get; set; }

        public string? Address { get; set; }
        public string? City1 { get; set; }
        public string? City2 { get; set; }
        public string? StateId { get; set; }
        public string? StateName { get; set; }

        public string? STDCode { get; set; }
        public string? PhNumber { get; set; }
        public int? Status { get; set; }
    }
}
