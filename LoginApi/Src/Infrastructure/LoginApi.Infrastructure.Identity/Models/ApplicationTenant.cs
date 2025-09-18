using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Infrastructure.Identity.Models;
using HyBrForex.Domain.Common;

namespace LoginApi.Infrastructure.Identity.Models
{
    public class ApplicationTenant: AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string ? DomainId { get; set; }
        public string ConnectionString { get; set; }
        [Required]
        [MaxLength(200)]
        public string CompanyName { get; set; }

        [MaxLength(200)]
        public string LegalName { get; set; }

        [MaxLength(50)]
        public string TaxId { get; set; }

        // Contact Information
        [MaxLength(200)]
        public string Address1 { get; set; }

        [MaxLength(200)]
        public string Address2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(20)]
        public string PostalCode { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

    }
}
