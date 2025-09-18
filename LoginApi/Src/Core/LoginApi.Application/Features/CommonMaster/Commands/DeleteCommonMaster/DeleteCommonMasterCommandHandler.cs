using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;

public class DeleteCommonMasterCommandHandler(
    ICommonMasterRepository commonMasterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommonMasterCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteCommonMasterCommand request, CancellationToken cancellationToken)
    {
        var master = await commonMasterRepository.GetByIdAsync(request.Id);

        if (master is null) return new Error(ErrorCode.NotFound, "Common master is not found", nameof(request.Id));
        master.Status = 0;
        commonMasterRepository.Update(master);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}