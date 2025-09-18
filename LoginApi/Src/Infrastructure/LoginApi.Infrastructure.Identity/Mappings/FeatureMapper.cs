using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Infrastructure.Identity.Models;
using LoginApi.Infrastructure.Identity.Models;
using MediatR;

namespace HyBrForex.Infrastructure.Identity.Mappings
{
    public static class FeatureMapper
    {
        public static ApplicationFeature ToEntity(FeatureRequest request)
        {
            return new ApplicationFeature
            {
                Id = Ulid.NewUlid().ToString(),
                FeatureName = request.FeatureName,
                Create = request.Create,
                Edit = request.Edit,
                Delete = request.Delete,
                View = request.View,
             
               
            };
        }

        public static FeatureResponse ToResponse(ApplicationFeature entity, List<FeatureRoleMapping> roles)
        {
            return new FeatureResponse
            {
                Id = Ulid.NewUlid().ToString(),
                FeatureName = entity.FeatureName,
                Create = entity.Create,
                Edit = entity.Edit,
                Delete = entity.Delete,
                View = entity.View,
                FunctionalId = roles.Select(r => r.FunctionalId).FirstOrDefault(),
                RoleIds = roles.Select(r => r.RoleId).ToList(),
            };
        }
    }
}

