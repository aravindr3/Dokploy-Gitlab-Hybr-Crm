using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Tenant.Responses
{
    public class TenantResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
