using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class BankMasterDto
    {
        public BankMasterDto() { }

        public BankMasterDto(Bankmasters bankMaster) 
        { 
            Id = bankMaster.Id;
            BankName = bankMaster.BankName;
            BankCode = bankMaster.BankCode;
            Status = bankMaster.Status;


        }
        public string Id { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public int Status { get; set; }
    }
}
