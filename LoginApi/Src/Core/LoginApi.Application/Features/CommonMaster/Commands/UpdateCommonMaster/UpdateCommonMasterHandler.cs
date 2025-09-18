using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;

public class UpdateCommonMasterHandler(
    ICommonMasterRepository commonMasterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommonMasterCommand, BaseResult>
{
    public async Task<BaseResult> Handle(UpdateCommonMasterCommand request, CancellationToken cancellationToken)
    {
        var master = await commonMasterRepository.GetByIdAsync(request.Id);

        if (master is null) return new Error(ErrorCode.NotFound, "CommonMaster not found", nameof(request.Id));

        master.Update(request.CommonTypeId, request.CommonName);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}