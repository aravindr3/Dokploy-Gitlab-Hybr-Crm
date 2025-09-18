using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class StateMasterDto
    {
        public StateMasterDto()
        {
        }

        public StateMasterDto(StateMaster stateMaster)
        {
            Id = stateMaster.Id;
            StateCode = stateMaster?.StateCode;
            StateName = stateMaster?.StateName;
            GSTCode = stateMaster?.GSTCode;
            CountryId =  stateMaster?.CountryId;
            CountryName = stateMaster?.Country?.CountryName;
            Status = stateMaster.Status;
        }

        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public int? GSTCode { get; set; }
        public string? CountryId { get; set; }
        public string ? CountryName { get; set; }
        public string? Id { get; set; }
        public int ? Status {  get; set; }
    }
}
