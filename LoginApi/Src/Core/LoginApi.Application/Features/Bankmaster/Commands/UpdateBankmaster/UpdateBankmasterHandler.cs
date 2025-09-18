using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Commands.UpdateBankmaster
{
    public class UpdateBankmasterHandler(
    IBankmasterRepository bankmasterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateBankmasterCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(UpdateBankmasterCommand request, CancellationToken cancellationToken)
        {
            var master = await bankmasterRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Bankmaster not found", nameof(request.Id));

            master.Update(request.BankName, request.BankCode);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}