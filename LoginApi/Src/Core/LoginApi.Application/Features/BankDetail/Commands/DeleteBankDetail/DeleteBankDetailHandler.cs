using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.Bankmaster.Commands.DeleteBankmaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Commands.DeleteBankDetail
{
    public class DeleteBankDetailHandler (IBankdetailsRepository bankdetailsRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteBankDetailCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteBankDetailCommand request, CancellationToken cancellationToken)
{
    var master = await bankdetailsRepository.GetByIdAsync(request.Id);

    if (master is null) return new Error(ErrorCode.NotFound, "Bank not found", nameof(request.Id));
    master.Status = 0;
            bankdetailsRepository.Update(master);
    await unitOfWork.SaveChangesAsync();

    return BaseResult.Ok();
}
    }
}