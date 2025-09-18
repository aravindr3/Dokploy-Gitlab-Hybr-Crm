using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HyBrCRM.Application.Features.ActivityLog.Query.GetAll
{
    public class GetAllActivityLogQueryHandler(ILeadContactRepository leadContactRepository,
        ILeadRepository leadRepository , IActivityLogServices activityLogServices ,
        IUserService userService,
        IDomainRepository domainRepository,
        IStagesServices stagesServices,
        IDomainStagesServices domainStagesServices,
        IVerticalServices verticalServices)
    : IRequestHandler<GetAllActivityLog, BaseResult<List<ActivityLogDto>>>
    {
        public async Task<BaseResult<List<ActivityLogDto>>> Handle(GetAllActivityLog request, CancellationToken cancellationToken)
        {
            var masters = await activityLogServices.GetAllAsync();
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any()) return new Error(ErrorCode.NotFound, "Activity History not found");

            var dtoList = new List<ActivityLogDto>();

            foreach (var activity in masterList.OrderByDescending(x => x.Created))
            {
                var lead = await leadRepository?.GetByIdAsync(activity?.LeadId);
                var leadContact = await leadContactRepository?.GetByIdAsync(lead?.LeadContactId);
                var leadName = leadContact?.FirstName;
                var dto = new ActivityLogDto
                {
                    Id = activity.Id,
                    LeadId = activity.LeadId,
                    LeadName = leadName,
                    OwnerId = activity.OwnerId,
                    OwnerName = userService?.GetUserByIdAsync(activity?.OwnerId)?.Result?.Data?.UserName,

                    ActivityTypeId = activity.ActivityTypeId,
                    ActivityTypeName = stagesServices.GetByIdAsync(domainStagesServices.GetByIdAsync(activity.ActivityTypeId).Result.StagesId).Result?.Name,
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
