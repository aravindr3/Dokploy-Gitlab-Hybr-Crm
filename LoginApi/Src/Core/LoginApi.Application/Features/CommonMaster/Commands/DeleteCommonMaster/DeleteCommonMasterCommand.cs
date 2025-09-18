using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.DeleteCommonMaster;

public class DeleteCommonMasterCommand : IRequest<BaseResult>
{
    public string Id { get; set; }
}