using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.UpdateCommonTypeMaster;

public class UpdateCommonTypeMasterHandler(
    ICommonTypeMsaterRepository commonTypeMsaterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommonTypeMasterCommand, BaseResult>
{
    public async Task<BaseResult> Handle(UpdateCommonTypeMasterCommand request, CancellationToken cancellationToken)
    {
        var commonTypeMsater = await commonTypeMsaterRepository.GetByIdAsync(request.Id);

        if (commonTypeMsater is null)
            return new Error(ErrorCode.NotFound, "Common type master is not found", nameof(request.Id));

        commonTypeMsater.Update(request.TypeName);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}