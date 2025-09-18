using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class StateMaster : AuditableBaseEntity
    {
        public StateMaster(string stateCode, string stateName, int? gSTCode)
        {
            StateCode = stateCode;
            StateName = stateName;
            GSTCode = gSTCode;
        }

        public string ? StateCode { get; set; }
        public string  ? StateName { get; set; }
        public int ? GSTCode {  get; set; }
        public string ? CountryId { get; set; }

        public virtual  CountryMaster ? Country {  get; set; }

    }
}
