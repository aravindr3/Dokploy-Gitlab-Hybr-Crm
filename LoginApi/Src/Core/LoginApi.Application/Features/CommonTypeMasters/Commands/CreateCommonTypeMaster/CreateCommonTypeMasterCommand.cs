using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.CreateCommonTypeMaster;

public class CreateCommonTypeMasterCommand : IRequest<UlidBaseResult<string>>
{
    public string TypeName { get; set; }
}