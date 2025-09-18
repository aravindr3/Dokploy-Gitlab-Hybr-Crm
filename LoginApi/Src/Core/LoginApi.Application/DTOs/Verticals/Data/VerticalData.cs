using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrCRM.Application.DTOs.Verticals.Data
{
    public class VerticalData
    {
        public string ? Id { get; set; }
        public string? TenantId { get; set; }
        public string ? TenantName { get; set; }
        public string? DomainId { get; set; }
        public string ? DomainName { get; set; }
        public string? VerticalName { get; set; }
        public int ? Status { get; set; }
    }
}
