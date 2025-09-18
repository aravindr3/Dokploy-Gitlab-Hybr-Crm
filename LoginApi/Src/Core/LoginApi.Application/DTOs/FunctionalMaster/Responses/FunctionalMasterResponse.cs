using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.FunctionalMaster.Responses
{
    public class FunctionalMasterResponse
    {
        public String Id { get; set; }
        public string FunctionalName { get; set; }
        public string RouterName { get; set; }
        public string RouterDescription { get; set; }
        public string RouterLink { get; set; }
        public int SequenceId { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public bool isHeader { get; set; }
        public bool isSubHeader { get; set; }
        public int Orderby { get; set; }
        public List<string> ChildRouterLinks { get; set; } = new List<string>();

    }
}
