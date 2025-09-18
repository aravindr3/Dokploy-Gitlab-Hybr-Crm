using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Feature.Request
{
    public class FeatureRoleMappingRequest
    {
        public string RoleId { get; set; } = default!;
        public List<FeaturePermissionDto> Permissions { get; set; } = new();
    }

    public class FeaturePermissionDto
    {
        public string FeatureId { get; set; } = default!;
        public bool Assign { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
        public bool Print { get; set; }
        public bool Approval { get; set; }
    }
}
