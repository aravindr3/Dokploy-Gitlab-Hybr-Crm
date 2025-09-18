using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Commands.CreateCurrencyMaster;

public class CreateCurrencyMasterCommand : IRequest<BaseResult<string>>
{
    public string CountryId { get; set; }
    public string ISD { get; set; }
    public string Currency { get; set; }
    public string FLM8Cd { get; set; }
}