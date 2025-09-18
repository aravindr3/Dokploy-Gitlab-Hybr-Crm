using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using HyBrCRM.Application.Interfaces.Repositories;
using System.Threading;
using HyBrForex.Domain.Exchange.Entities;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Application.Features.LeadContact.Command.Create
{
    internal class CreateLeadContactCommandHandler(
    ILeadContactRepository leadContactRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateLeadContactCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateLeadContactCommand request,
       CancellationToken cancellationToken)
        {
            var stateId = request.StateId;
            if (stateId == "") {
                stateId = "01JJ6JTZPM2N1RQBHKSGCNVE0";
            }

            var GenderId = request.GenderId;
            if (GenderId == "")
            {
                GenderId = "01JXVQH4EANY2GCTS8PDR5PWKS1";
            }
            string countryId = request.CountryId;
            if (countryId == "")
            {
                countryId = "01JJ6JTZPM2N1RQBHKSGCNVE010";
            }

            var master = new Domain.Exchange.Entities.LeadContact(
                request. FirstName,
       request.LastName ,
        request.PhoneNumber1,
            request.PhoneNumber2,
            request.WhatsAppNumber ,
            request.Email,
            GenderId,
            request.ParentsName,
            request.ParentsPhoneNumber,
            countryId,
            stateId,
            request.StateOutsideIndia,
            request.District,
            request.City,
            request.Locality,
            request.VerticalId


                );
            master.Id = Ulid.NewUlid().ToString();
            await leadContactRepository.AddAsync(master);
            await unitOfWork.SaveChangesAsync();

            return master.Id;
        }
    }
}
