using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class Bankdetails: AuditableBaseEntity
    {
        public Bankdetails() { }

        public Bankdetails(string bankId,string iFSCCode,string branch,string address,string city1,string city2,string stateId,string sTDCode,string phNumber) 
        {
            BankId = bankId;    
            IFSCCode = iFSCCode;
            Branch = branch;
            Address = address;
            City1 = city1;
            City2 = city2;
            StateId = stateId;
            STDCode = sTDCode;
            PhNumber = phNumber;
        }

        public string? BankId { get; set; }
        public string? IFSCCode { get; set; }
        public string? Branch { get; set; }

        public string? Address { get; set; }
        public string? City1 { get; set; }
        public string? City2 { get; set; }
        public string? StateId { get; set; }
       
        public string? STDCode { get; set; }
        public string? PhNumber { get; set; }

        public virtual Bankmasters? Bank { get; set; }
        public virtual StateMaster? State { get; set; }


        public void Update(string bankId, string iFSCCode, string branch, string address, string city1, string city2, string stateId, string sTDCode, string phNumber)
        {
            BankId = bankId;
            IFSCCode = iFSCCode;
            Branch = branch;
            Address = address;
            City1 = city1;
            City2 = city2;
            StateId = stateId;
            STDCode = sTDCode;
            PhNumber = phNumber;
        }
    }
}
