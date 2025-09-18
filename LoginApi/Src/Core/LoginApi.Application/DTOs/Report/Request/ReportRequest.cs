using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Report.Request
{
    public class ReportRequest
    {
        public  string Name { get; set; }
        public  string Query { get; set; }
        public string? ParentId { get; set; }
        public int Orderby { get; set; }
        public int SequenceId { get; set; }
        public string? Type { get; set; }
        public string Align { get; set; }
        public string Category { get; set; }
    }
}
