using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.Bankmaster.Commands.UpdateBankmaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Commands.UpdateBankDetail
{
    public class UpdateBankDetailHandler(
    IBankdetailsRepository bankdetailsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateBankDetailCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateBankDetailCommand request, CancellationToken cancellationToken)
        {
            var master = await bankdetailsRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Bankdetails not found", nameof(request.Id));

            master.Update(request.BankId, request.IFSCCode, request.Branch, request.Address, request.City1, request.City2, request.StateId, request.STDCode, request.PhNumber);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}