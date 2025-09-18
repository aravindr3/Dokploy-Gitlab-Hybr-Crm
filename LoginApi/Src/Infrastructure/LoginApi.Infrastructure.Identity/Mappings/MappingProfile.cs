using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.DTOs.Tenant.Requests;
using AutoMapper;
using HyBrForex.Application.DTOs.Tenant.Responses;
using LoginApi.Infrastructure.Identity.Models;
using HyBrForex.Application.DTOs.FunctionalMaster.Requests;
using HyBrForex.Application.DTOs.FunctionalMaster.Responses;
using HyBrForex.Application.DTOs.Feature.Request;
using HyBrForex.Infrastructure.Identity.Models;
using HyBrForex.Application.DTOs.Feature.Response;
using HyBrForex.Application.DTOs.Feedback.Request;
using HyBrForex.Application.DTOs.Feedback.Response;
using HyBrForex.Application.DTOs.Report.Request;
using HyBrForex.Application.DTOs.Report.Response;

namespace HyBrForex.Infrastructure.Identity.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateRequest, ApplicationTenant>()
             .ForMember(dest => dest.Id, opt => opt.Ignore()); // Id is set explicitly using Ulid.NewUlid()

            // Mapping UpdateRequest -> ApplicationTenant (for updating existing tenants)
            CreateMap<UpdateRequest, ApplicationTenant>();

            // Mapping ApplicationTenant -> TenantResponse
            CreateMap<ApplicationTenant, TenantResponse>();

            // Mapping ApplicationTenant -> GetApplicationTenantResponse (for retrieval)
            CreateMap<ApplicationTenant, GetApplicationTenantResponse>();

            CreateMap<FunctionalMasterRequest, FunctionalMaster>(); // Request to Entity
            CreateMap<FunctionalMaster, FunctionalMaster>();
            CreateMap<FunctionalMaster, FunctionalMasterResponse>();
            CreateMap<FeatureRequest, ApplicationFeature>(); // Request to Entity
            CreateMap<ApplicationFeature, ApplicationFeature>();
            CreateMap<ApplicationFeature, FeatureResponse>();
            CreateMap<FeedbackRequest, Feedback>();
            CreateMap<Feedback, FeedbackResponse>();
            CreateMap<ReportFeatureRequest, ReportFeature>(); // Request to Entity
            CreateMap<ReportFeature, ReportFeature>();
            CreateMap<ReportFeature, ReportFeatureResponse>();

        }


    }
}
