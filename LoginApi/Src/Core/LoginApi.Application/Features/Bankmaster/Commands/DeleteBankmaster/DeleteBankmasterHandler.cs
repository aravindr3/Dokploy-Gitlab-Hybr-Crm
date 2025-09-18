using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Features.Bankmaster.Commands.CreateBankmaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;
using HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;
using System.Threading;

namespace HyBrForex.Application.Features.Bankmaster.Commands.DeleteBankmaster
{
    public class DeleteBankmasterHandler(IBankmasterRepository bankmasterRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteBankmasterCommand, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteBankmasterCommand request, CancellationToken cancellationToken)
        {
            var master = await bankmasterRepository.GetByIdAsync(request.Id);

            if (master is null) return new Error(ErrorCode.NotFound, "Bank not found", nameof(request.Id));
            master.Status = 0;
            bankmasterRepository.Update(master);
            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}
