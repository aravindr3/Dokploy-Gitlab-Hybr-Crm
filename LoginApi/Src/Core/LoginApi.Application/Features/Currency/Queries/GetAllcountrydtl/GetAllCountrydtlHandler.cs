using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetAllcountrydtl;

public class GetAllCountrydtlHandler(ICountryRepository countryRepository)
    : IRequestHandler<GetAllCountrydtlQuery, BaseResult<List<CountryMasterDto>>>
{
    public async Task<BaseResult<List<CountryMasterDto>>> Handle(GetAllCountrydtlQuery request,
        CancellationToken cancellationToken)
    {
        var c = await countryRepository.GetAllAsync();
        if (c is null) return new Error(ErrorCode.NotFound, "Country master is not found");

        var currencymasterdtos = c
            .Select(c => new CountryMasterDto
            {
                Id = c.Id,
                CountryName = c.CountryName,
                SwiftCode = c.SwiftCode,
                Status = c.Status
            }).Where(a => a.Status == 1)
            .ToList();

        return BaseResult<List<CountryMasterDto>>.Ok(currencymasterdtos);
    }
}