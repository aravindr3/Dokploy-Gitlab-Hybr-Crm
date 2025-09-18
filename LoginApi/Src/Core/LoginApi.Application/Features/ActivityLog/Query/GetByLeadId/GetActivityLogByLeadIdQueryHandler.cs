using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.ActivityLog.Query.GetByLeadId
{
    public class GetActivityLogByLeadIdQueryHandler(ILeadContactRepository leadContactRepository,
        IUserService userService,
        IStagesServices stagesServices,
        IDomainRepository domainRepository,
        IDomainStagesServices domainStagesServices,
        ILeadRepository leadRepository,
        IHolidaysLeadServices holidaysLeadServices,
        IActivityLogServices activityLogServices)
    : IRequestHandler<GetActivityLogByLeadIdQuery, BaseResult<List<ActivityLogDto>>>
    {
        public async Task<BaseResult<List<ActivityLogDto>>> Handle(GetActivityLogByLeadIdQuery request, CancellationToken cancellationToken)
        {
            var masters = await activityLogServices.GetByIdChildAsync(a=> a.LeadId == request.LeadId);
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any()) return new Error(ErrorCode.NotFound, "Activity History not found");

            var dtoList = new List<ActivityLogDto>();
            var studyLead = await leadRepository.GetByIdAsync(request.LeadId);
            var holidayLead = await holidaysLeadServices.GetByIdAsync(request.LeadId);
            var lead = "";
            if(request.DomainId== "01JXF2E06WSBC19T902TN878TH")
            {
                lead = studyLead.LeadContactId;
            }
            if (request.DomainId == "01JZT3J2CSEKGNJPZ748WNW7J3")
            {
                lead = holidayLead.LeadContactId;
            }

            var leadContact =await leadContactRepository?.GetByIdAsync(lead);
            var leadName = leadContact?.FirstName;


            foreach (var activity in masterList.OrderByDescending(x => x.Created))
            {

                var dto = new ActivityLogDto
                {

                    Id = activity.Id,
                    LeadId = activity.LeadId,
                    LeadName = leadName,
                    OwnerId = activity.OwnerId,
                    OwnerName = userService?.GetUserByIdAsync(activity?.OwnerId)?.Result?.Data?.UserName,

                    ActivityTypeId = activity.ActivityTypeId,
                    ActivityTypeName =stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(activity.ActivityTypeId).Result.StagesId).Result?.Name,
                    Description = activity.Description,
                    ActivityDate = activity.ActivityDate,
                    CallType = activity?.CallType,
                    Notes = activity?.Notes,
                    Status = activity.Status
                   
                };
                dtoList.Add(dto);
            }

            return BaseResult<List<ActivityLogDto>>.Ok(dtoList);
        }

    
    }
}
