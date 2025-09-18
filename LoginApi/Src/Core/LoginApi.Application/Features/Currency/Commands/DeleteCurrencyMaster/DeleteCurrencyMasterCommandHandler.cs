using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Commands.DeleteCurrencyMaster;

public class DeleteCurrencyMasterCommandHandler(
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCurrencyMasterCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteCurrencyMasterCommand request, CancellationToken cancellationToken)
    {
        var currency = await currencyRepository.GetByIdAsync(request.Id);

        if (currency is null) return new Error(ErrorCode.NotFound, "Currency master is not found", nameof(request.Id));

        currency.Status = 0;
        currencyRepository.Update(currency);

        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}