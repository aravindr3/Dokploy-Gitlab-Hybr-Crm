using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;

namespace HyBrForex.Application.DTOs.Feature.Response
{
    public class FeatureRoleResponse
    {
        public string RoleId { get; set; }
        public List<FeaturePermissionDto> Permissions { get; set; }
    }
}
