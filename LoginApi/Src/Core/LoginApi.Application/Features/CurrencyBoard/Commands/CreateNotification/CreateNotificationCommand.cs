using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CurrencyBoard.Commands.CreateNotification;

public class CreateNotificationCommand : IRequest<BaseResult<string>>
{
    public string Notification { get; set; }
    public int Status { get; set; }
}