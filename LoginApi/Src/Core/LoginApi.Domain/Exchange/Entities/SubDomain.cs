using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class SubDomain : AuditableBaseEntity
    {
        public SubDomain(string domainId , string categoryName)
        {
            DomainId = domainId;
            CategoryName = categoryName;
        }

        public string ? DomainId { get; set; }
        public string ? CategoryName { get; set; }
    }
}
