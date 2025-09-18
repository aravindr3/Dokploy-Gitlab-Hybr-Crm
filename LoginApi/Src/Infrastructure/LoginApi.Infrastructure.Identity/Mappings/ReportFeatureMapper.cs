using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.Report.Request;
using HyBrForex.Application.DTOs.Report.Response;
using HyBrForex.Infrastructure.Identity.Models;
using MediatR;

namespace HyBrForex.Infrastructure.Identity.Mappings
{
    public class ReportFeatureMapper
    {
        public static ApplicationReport ToEntity(ReportFeatureRequest request)
        {
            return new ApplicationReport
            {
                Id = Ulid.NewUlid().ToString(),
                ReportFeatureName = request.ReportFeaturName,
                Csv = request.Csv,
                Excel = request.Excel,
                PDF = request.PDF,
                Print = request.Print,


            };
        }

        public static ReportFeatureResponse ToResponse(ApplicationReport entity, List<ReportRoleMapping> roles)
        {
            return new ReportFeatureResponse
            {
                Id = Ulid.NewUlid().ToString(),
                Csv = entity.Csv,
                Excel = entity.Excel,
                PDF = entity.PDF,
                Print = entity.Print,
                ReportId = roles.Select(r => r.ReportID).FirstOrDefault(),
                RoleIds = roles.Select(r => r.RoleId).ToList(),
            };
        }
    }
}
