using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Feature.Response
{
    public class FearureRoleMappingResponse
    {
        public String Id { get; set; }
        public string FeatureId { get; set; } = default!;
        public bool Assign { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool Print { get; set; }
        public bool Approval { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
