using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetCurrencyMasterById;

public class GetCurrencyMasterByIdQueryHandler(ICurrencyRepository currencyRepository)
    : IRequestHandler<GetCurrencyMasterByIdQuery, BaseResult<CurrencyMasterDto>>
{
    public async Task<BaseResult<CurrencyMasterDto>> Handle(GetCurrencyMasterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var currencyMaster = await currencyRepository.GetByIdAsync(request.Id);

        if (currencyMaster is null || currencyMaster.Status == 0)
            return new Error(ErrorCode.NotFound, "Common type master is not found", nameof(request.Id));

        return new CurrencyMasterDto(currencyMaster);
    }
}