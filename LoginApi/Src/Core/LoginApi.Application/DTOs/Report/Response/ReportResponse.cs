using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Report.Response
{
    public class ReportResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Query { get; set; } = default!;
        public string? ParentId { get; set; }
        public int Orderby { get; set; }
        public int SequenceId { get; set; }
        public string? Type { get; set; }
        public string Category { get; set; }
    }
}

