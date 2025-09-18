using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class SubDomainDto
    {
        public SubDomainDto()
        {
        }

        public SubDomainDto(SubDomain subDomain)
        {
            Id = subDomain.Id;
            DomainId = subDomain.DomainId;
            CategoryName = subDomain?.CategoryName;
            Status = subDomain.Status;
        }

        public string ? Id { get; set; }
        public string? DomainId { get; set; }
        public string ? SubDomainName { get; set; }
        public string ? DomainName { get; set; }
        public string? CategoryName { get; set; }
        public int Status { get; set; }
    }
}
