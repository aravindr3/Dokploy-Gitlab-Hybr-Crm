using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.UpdateCommonTypeMaster;

public class UpdateCommonTypeMasterCommand : IRequest<BaseResult>
{
    public string Id { get; set; }
    public string TypeName { get; set; }
}