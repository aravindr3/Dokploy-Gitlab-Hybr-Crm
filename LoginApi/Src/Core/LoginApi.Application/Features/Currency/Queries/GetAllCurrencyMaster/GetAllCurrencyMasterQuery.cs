using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetAllCurrencyMaster;

public class GetAllCurrencyMasterQuery : IRequest<BaseResult<List<CurrencyMasterDto>>>
{
}