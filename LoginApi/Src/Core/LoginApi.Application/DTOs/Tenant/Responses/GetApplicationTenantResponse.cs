using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Tenant.Responses
{
    public class GetApplicationTenantResponse
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string DomainId { get; set; }
        public string DomainName { get; set; }
        public string ConnectionString { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
