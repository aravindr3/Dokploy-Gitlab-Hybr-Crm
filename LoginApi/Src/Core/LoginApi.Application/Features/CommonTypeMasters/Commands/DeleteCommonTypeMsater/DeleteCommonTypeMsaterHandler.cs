using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;


namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.DeleteCommonTypeMsater
{

    public class DeleteCommonTypeMsaterHandler(
        ICommonTypeMsaterRepository commonTypeMsaterRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommonTypeMsater, BaseResult>
    {
        public async Task<BaseResult> Handle(DeleteCommonTypeMsater request, CancellationToken cancellationToken)
        {
            var commonTypeMsater = await commonTypeMsaterRepository.GetByIdAsync(request.Id);

            if (commonTypeMsater is null)
                return new Error(ErrorCode.NotFound, "Common type master is not found", nameof(request.Id));


            commonTypeMsater.Status = 0;
            commonTypeMsaterRepository.Update(commonTypeMsater);

            await unitOfWork.SaveChangesAsync();

            return BaseResult.Ok();
        }
    }
}