using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Command.Create
{
    public class CreateLeadCommandHandler(
    ILeadRepository leadRepository,
    ILeadPropertiesValueServices leadPropertiesValueServices,
    IleadPropertyDefinitionServices leadPropertyDefinitionServices,
    IUserService userService,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateLeadCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateLeadCommand request,
       CancellationToken cancellationToken)
        {
            var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
            if (ownerId == null)
            {
                return new Error(ErrorCode.NotFound, "User not found");
            }
            string countryId = request.PreferedCountry;
            if (countryId == "")
            {
                countryId = "01JJ6JTZPM2N1RQBHKSGCNVE010";
            }
            var master = new Domain.Exchange.Entities.Lead(
             request. LeadContactId,
        request.LeadSourceId ,
            "01JYJY1J0T6247H5ZCXCESYSX21",
            request.VerticalId ,
            request.Notes ,
            request?.CategoryId,
            countryId


                );
            master.Id = Ulid.NewUlid().ToString();

            // 2. Create lead property value for "CountryInterestedIn"
            //if (!string.IsNullOrEmpty(request.PreferedCountry))
            //{
            //    var propertyDefinition = await leadPropertyDefinitionServices.GetByIdAsync("01JZT20S7BZKW3YGM05S5GVFY2");
            //    if (propertyDefinition != null)
            //    {
            //        var leadProperty = new  LeadProperyValue
            //        {
            //            Id = Ulid.NewUlid().ToString(),
            //            LeadId = master.Id,
            //            OwnerId = request?.OwnerId,
            //            PropertyDefinitionId = propertyDefinition.Id,
            //            Value = request.PreferedCountry,

            //        };

            //        await leadPropertiesValueServices.AddAsync(leadProperty);
            //        await unitOfWork.SaveChangesAsync();
            //    }
            //}
            var leadProperty = new LeadProperyValue
            {
                Id = Ulid.NewUlid().ToString(),
                LeadId = master.Id,
                OwnerId = request?.OwnerId
            };

            // If PreferedCountry is provided, add PropertyDefinition and Value
            if (!string.IsNullOrEmpty(request.PreferedCountry))
            {
                var propertyDefinition = await leadPropertyDefinitionServices.GetByIdAsync("01JZT20S7BZKW3YGM05S5GVFY2");
                if (propertyDefinition != null)
                {
                    leadProperty.PropertyDefinitionId = propertyDefinition.Id;
                    leadProperty.Value = request.PreferedCountry;
                }
            }

            await leadPropertiesValueServices.AddAsync(leadProperty);
            await unitOfWork.SaveChangesAsync();


            await leadRepository.AddAsync(master);
            await unitOfWork.SaveChangesAsync();


            return master.Id;
        }
    
    }
}
