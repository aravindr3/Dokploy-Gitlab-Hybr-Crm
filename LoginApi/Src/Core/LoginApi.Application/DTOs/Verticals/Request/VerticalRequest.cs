using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrCRM.Application.DTOs.Verticals.Request
{
    public class VerticalRequest
    {
        public string ? TenantId { get; set; }
        public string ? DomainId { get; set; }
        public string ? VerticalName { get; set; }
    }
}
