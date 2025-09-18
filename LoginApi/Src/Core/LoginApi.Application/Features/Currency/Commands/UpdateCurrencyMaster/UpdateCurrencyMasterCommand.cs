using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Commands.UpdateCurrencyMaster;

public class UpdateCurrencyMasterCommand : IRequest<BaseResult>
{
    public string Id { get; set; }
    public string ISD { get; set; }
    public string Currency { get; set; }
    public string FLM8Cd { get; set; }
}