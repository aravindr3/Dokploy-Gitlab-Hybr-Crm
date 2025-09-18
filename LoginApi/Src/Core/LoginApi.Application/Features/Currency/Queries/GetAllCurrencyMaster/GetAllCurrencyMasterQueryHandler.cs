using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetAllCurrencyMaster;

public class GetAllCurrencyMasterQueryHandler(ICurrencyRepository currencyRepository, ICountryRepository countryRepository)
    : IRequestHandler<GetAllCurrencyMasterQuery, BaseResult<List<CurrencyMasterDto>>>
{
    public async Task<BaseResult<List<CurrencyMasterDto>>> Handle(GetAllCurrencyMasterQuery request,
        CancellationToken cancellationToken)
    {
        var currencyMasters = await currencyRepository.GetAllWithChildAsync(a=>a.CountryMaster);
        if (currencyMasters is null) return new Error(ErrorCode.NotFound, "Common type master is not found");

        var currencymasterdtos = currencyMasters
            .Select(c => new CurrencyMasterDto()
            {
                CountryId = c.CountryId,
                CountryName = countryRepository.GetByIdAsync(c.CountryId).Result?.CountryName,
                Currency=c.Currency,
                Id = c.Id,
                FLM8Cd = c.FLM8Cd,
                ISD = c.ISD,
                Status = c.Status
            }).Where(a => a.Status == 1)
            .ToList();

        return BaseResult<List<CurrencyMasterDto>>.Ok(currencymasterdtos);
    }
}