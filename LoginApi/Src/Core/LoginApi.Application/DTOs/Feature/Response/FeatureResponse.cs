using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Feature.Response
{
    public class FeatureResponse
    {
        public String Id { get; set; }
        public string FeatureName { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool Approval { get; set; }
        public string FunctionalId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
