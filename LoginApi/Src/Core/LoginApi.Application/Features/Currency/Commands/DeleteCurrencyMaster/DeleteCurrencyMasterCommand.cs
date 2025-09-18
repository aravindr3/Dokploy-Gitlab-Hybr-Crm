using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Currency.Commands.DeleteCurrencyMaster;

public class DeleteCurrencyMasterCommand : IRequest<BaseResult>
{
    public string Id { get; set; }
}