using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HyBrForex.Infrastructure.Identity.Models
{
    public class Report :  AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public string ParentId { get; set; }
        public string Category { get; set; }
        public int Orderby { get; set; }
        public int SequenceId { get; set; }
        public string Type { get; set; }
    }
}
