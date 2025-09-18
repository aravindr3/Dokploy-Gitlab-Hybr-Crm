using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.HolidaysLead.Command.Update
{
    public class UpdateHolidayLeadCommandHandler(
    IHolidaysLeadServices holidaysLeadServices,
    IUserService userService,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateHolidayLeadCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateHolidayLeadCommand request, CancellationToken cancellationToken)
        {
            var master = await holidaysLeadServices.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Lead not found", nameof(request.Id));
            var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
            if (ownerId == null)
            {
                return new Error(ErrorCode.NotFound, "User not found");
            }
            master.Update(
                             request.LeadContactId,
       request.EnquiryType,
            request.TravelType,
            request.PrefferedDestination,
            request.TripDuration,
            request.DepatureCity,
            request.StartDate,
            request.EndDate,
            request.Adults,
            request.ChildWithBed,
            request.ChildWithoutBed,
            request.BeddingPreference,
            request.RoomType,
            request.MealPlan,
            request.Notes,
            request.FollowUpDate,
            request.AsignedAgent,
            request.VerticalId,
            request.LeadSourceId,
            request.LeadStatusId,
            request?.CategoryId
         
                );
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }

    }
   
}
