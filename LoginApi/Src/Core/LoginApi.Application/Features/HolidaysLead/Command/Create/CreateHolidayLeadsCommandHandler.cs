using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.Lead.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.HolidaysLead.Command.Create
{
    public class CreateHolidayLeadsCommandHandler(
    IHolidaysLeadServices holidaysLeadServices,ILeadPropertiesValueServices leadPropertiesValueServices,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    IUserService userService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateHolidayLeadCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateHolidayLeadCommand request,
       CancellationToken cancellationToken)
        {
            var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
            if (ownerId == null) {
                return new Error(ErrorCode.NotFound, "User not found");
            }
            var master = new Domain.Exchange.Entities.HoliDaysLead(
             request. LeadContactId ,
       request.EnquiryType ,
            request.TravelType ,
            request.PrefferedDestination ,
            request.TripDuration,
            request.DepatureCity ,
            request.StartDate,
            request.EndDate,
            request.Adults,
            request.ChildWithBed,
            request.ChildWithoutBed,
            request.BeddingPreference,
            request.RoomType,
            request.MealPlan,
            request.Notes ,
            request.FollowUpDate,
            request.AsignedAgent,
            request.VerticalId,
            request?.LeadSourceId,
            request?.LeadStatusId,
            request?.CategoryId,
            request?.Other


                );
            master.Id = Ulid.NewUlid().ToString();

            //if (!string.IsNullOrEmpty(request.PrefferedDestination))
            //{
            //    var propertyDefinition = await leadPropertyDefinitionServices.GetByIdAsync("01JZT20S7BZKW3YGM05S5GVFY2");
            //    if (propertyDefinition != null)
            //    {
            //        var leadProperty = new LeadProperyValue
            //        {
            //            Id = Ulid.NewUlid().ToString(),
            //            LeadId = master.Id,
            //            OwnerId = request?.OwnerId,
            //            PropertyDefinitionId = propertyDefinition.Id,
            //            Value = request.PrefferedDestination,

            //        };

            //        await leadPropertiesValueServices.AddAsync(leadProperty);
            //        await unitOfWork.SaveChangesAsync();
            //    }
            //}
            var leadProperty = new LeadProperyValue
            {
                Id = Ulid.NewUlid().ToString(),
                LeadId = master.Id,
                OwnerId = request?.AsignedAgent,
            };

            // If PreferedCountry is provided, add PropertyDefinition and Value
            if (!string.IsNullOrEmpty(request.PrefferedDestination))
            {
                var propertyDefinition = await leadPropertyDefinitionServices.GetByIdAsync("01JZT223RGMGW12HZH9P4VFBJK1");
                if (propertyDefinition != null)
                {
                    leadProperty.PropertyDefinitionId = propertyDefinition.Id;
                    leadProperty.Value = request.PrefferedDestination;
                }
            }

            await leadPropertiesValueServices.AddAsync(leadProperty);
            await unitOfWork.SaveChangesAsync();


            await holidaysLeadServices.AddAsync(master);
            await unitOfWork.SaveChangesAsync();
            return master.Id;
        }

    }
  
}
