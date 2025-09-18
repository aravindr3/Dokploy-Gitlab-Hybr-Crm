using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class Bankmasters : AuditableBaseEntity
    {
        public Bankmasters() { }
        public Bankmasters(string bankName,string bankCode) 
        {
            BankName = bankName;
            BankCode = bankCode;
        }
        public string BankName { get; set; }
        public string BankCode { get; set; }

        public void Update(string bankName, string bankCode)
        {
            BankName = bankName;
            BankCode = bankCode;
        }
    }
}
