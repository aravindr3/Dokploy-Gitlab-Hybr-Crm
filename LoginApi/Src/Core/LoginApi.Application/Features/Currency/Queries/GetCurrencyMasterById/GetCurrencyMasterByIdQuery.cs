using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Queries.GetCurrencyMasterById;

public class GetCurrencyMasterByIdQuery : IRequest<BaseResult<CurrencyMasterDto>>
{
    public string Id { get; set; }
}