using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Commands.UpdateCurrencyMaster;

public class UpdateCurrencyMasterCommandHandler(
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCurrencyMasterCommand, BaseResult>
{
    public async Task<BaseResult> Handle(UpdateCurrencyMasterCommand request, CancellationToken cancellationToken)
    {
        var currencyMaster = await currencyRepository.GetByIdAsync(request.Id);

        if (currencyMaster is null)
            return new Error(ErrorCode.NotFound, "Currency master is not found", nameof(request.Id));

        currencyMaster.Update(request.ISD, request.Currency, request.FLM8Cd);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}