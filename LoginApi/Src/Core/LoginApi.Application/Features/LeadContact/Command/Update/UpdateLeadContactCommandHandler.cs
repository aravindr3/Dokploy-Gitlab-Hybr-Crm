using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadContact.Command.Update
{
    public class UpdateLeadContactCommandHandler(
    ILeadContactRepository leadContactRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadContactCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateLeadContactCommand request, CancellationToken cancellationToken)
        {
            var master = await leadContactRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "LeadContact not found", nameof(request.Id));
            var stateId = request.StateId;
            if (stateId == "")
            {
                stateId = "01JJ6JTZPM2N1RQBHKSGCNVE0";
            }
            var GenderId = request.GenderId;
            if (GenderId == "")
            {
                GenderId = "01JXVQH4EANY2GCTS8PDR5PWKS1";
            }
            master.Update(
                                request.FirstName,
       request.LastName,
        request.PhoneNumber1,
            request.PhoneNumber2,
            request.WhatsAppNumber,
            request.Email,
            GenderId,
            request.ParentsName,
            request.ParentsPhoneNumber,
            request.CountryId,
            stateId,
            request.StateOutsideIndia,
            request.District,
            request.City,
            request.Locality,
            request.VerticalId
                );
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}
