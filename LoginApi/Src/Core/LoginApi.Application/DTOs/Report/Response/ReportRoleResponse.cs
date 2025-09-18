using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Report.Request;

namespace HyBrForex.Application.DTOs.Report.Response
{
    public class ReportRoleResponse
    {
        public string RoleId { get; set; }
        public List<ReportPermissionDto> Permissions { get; set; }
    }
}
