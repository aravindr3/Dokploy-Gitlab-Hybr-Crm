using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using HyBrCRM.Application.Interfaces.Repositories;

namespace HyBrCRM.Application.Features.LeadContact.Command.Delete
{
    public class DeleteLeadContactCommandHandler(
    ILeadContactRepository leadContactRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteLeadContactCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteLeadContactCommand request, CancellationToken cancellationToken)
        {
            var master = await leadContactRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Lead Contact is not found", nameof(request.Id));
            master.Status = 0;
            leadContactRepository.Update(master);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
  
}
