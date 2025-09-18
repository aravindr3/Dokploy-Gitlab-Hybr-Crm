using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;

namespace HyBrForex.Application.DTOs.FunctionalMaster.Responses
{
    public class RouterLinkDto
    {
        public string FunctionalId { get; set; }
        public string RouterLink { get; set; }

        // Permissions (nullable for backward compatibility)
        public bool? Assign { get; set; }
        public bool? Create { get; set; }
        public bool? Edit { get; set; }
        public bool? Delete { get; set; }
        public bool? View { get; set; }
        public bool? Print { get; set; }
        public bool? Approval { get; set; }
    }

    public class GetRouterLinksResponse
    {
        public List<RouterLinkDto> RouterLinks { get; set; } = new();
    }


}
