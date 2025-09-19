using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Update;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.HolidaysLead.Command.Update
{
    //public class UpdateHolidayLeadCommandHandler(
    //IHolidaysLeadServices holidaysLeadServices,
    //IUserService userService,
    //IUnitOfWork unitOfWork) : IRequestHandler<UpdateHolidayLeadCommand, BaseResult>
    //{
    //    public async Task<BaseResult> Handle(UpdateHolidayLeadCommand request, CancellationToken cancellationToken)
    //    {
    //        var master = await holidaysLeadServices.GetByIdAsync(request.Id);

    //        if (master is null) return new Error(ErrorCode.NotFound, "Lead not found", nameof(request.Id));
    //        var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
    //        if (ownerId == null)
    //        {
    //            return new Error(ErrorCode.NotFound, "User not found");
    //        }
    //        master.Update(
    //                         request.LeadContactId,
    //   request.EnquiryType,
    //        request.TravelType,
    //        request.PrefferedDestination,
    //        request.TripDuration,
    //        request.DepatureCity,
    //        request.StartDate,
    //        request.EndDate,
    //        request.Adults,
    //        request.ChildWithBed,
    //        request.ChildWithoutBed,
    //        request.BeddingPreference,
    //        request.RoomType,
    //        request.MealPlan,
    //        request.Notes,
    //        request.FollowUpDate,
    //        request.AsignedAgent,
    //        request.VerticalId,
    //        request.LeadSourceId,
    //        request.LeadStatusId,
    //        request?.CategoryId

    //            );
    //        await unitOfWork.SaveChangesAsync();

    //        return BaseResult.Ok();
    //    }

    //}
    public class UpdateHolidayLeadCommandHandler(
      IHolidaysLeadServices holidaysLeadServices,
      ILeadPropertiesValueServices leadPropertiesValueServices,
      IleadPropertyDefinitionServices leadPropertyDefinitionServices,
      IUserService userService,
      IUnitOfWork unitOfWork
  ) : IRequestHandler<UpdateHolidayLeadCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateHolidayLeadCommand request, CancellationToken cancellationToken)
        {
            var master = await holidaysLeadServices.GetByIdAsync(request.Id);

            if (master is null)
                return new Error(ErrorCode.NotFound, "Lead not found", nameof(request.Id));

            var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
            if (ownerId == null)
                return new Error(ErrorCode.NotFound, "User not found");

            // Update main entity
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
                request?.CategoryId,
                request?.Other
            );

            // ---- LeadProperty Update Logic ----
            if (!string.IsNullOrEmpty(request.PrefferedDestination))
            {
                // Same PropertyDefinition as in Create handler
                var propertyDefinition = await leadPropertyDefinitionServices.GetByIdAsync("01JZT223RGMGW12HZH9P4VFBJK1");

                if (propertyDefinition != null)
                {
                    // Check if property already exists
                    var existingProperty = await leadPropertiesValueServices
                        .GetByLeadAndPropertyAsync(master.Id, propertyDefinition.Id);

                    if (existingProperty != null)
                    {
                        // Update existing property
                        existingProperty.Value = request.PrefferedDestination;
                        existingProperty.OwnerId = request.AsignedAgent;
                         leadPropertiesValueServices.Update(existingProperty);
                    }
                    else
                    {
                        // Create new property if not exists
                        var newProperty = new LeadProperyValue
                        {
                            Id = Ulid.NewUlid().ToString(),
                            LeadId = master.Id,
                            OwnerId = request.AsignedAgent,
                            PropertyDefinitionId = propertyDefinition.Id,
                            Value = request.PrefferedDestination
                        };
                        await leadPropertiesValueServices.AddAsync(newProperty);
                    }
                }
            }

            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}
   

