using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetAllcountrydtl;

public class GetAllCountrydtlQuery : IRequest<BaseResult<List<CountryMasterDto>>>
{
}