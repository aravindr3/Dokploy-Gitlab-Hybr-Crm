using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.LeadContact.Command.Delete;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Command.Delete
{
    public class DeleteLeadCommandHandler(
    ILeadRepository leadRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteLeadCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
        {
            var master = await leadRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Lead  is not found", nameof(request.Id));
            master.Status = 0;
            leadRepository.Update(master);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
   
}
