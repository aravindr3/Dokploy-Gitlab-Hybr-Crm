using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Command.Update;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Command.Update
{
    public class UpdateLeadCommandHandler(
    ILeadRepository leadRepository,
    IUserService userService,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateLeadCommand request, CancellationToken cancellationToken)
        {
            var master = await leadRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Lead not found", nameof(request.Id));
            var ownerId = await userService.GetUserByIdAsync(request.OwnerId);
            if (ownerId == null)
            {
                return new Error(ErrorCode.NotFound, "User not found");
            }
            master.Update(
                             request?. LeadContactId ,
            request?.LeadSourceId,
            "01JYJY1J0T6247H5ZCXCESYSX21",
            request?.VerticalId,
            request?.Notes,
            request? .CategoryId,
            request?.PreferedCountry
                );
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    
    }
}
