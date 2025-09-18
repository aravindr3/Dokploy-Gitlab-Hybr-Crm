using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;

public class CreateCommonMasterCommand : IRequest<UlidBaseResult<string>>
{
    public string CommonTypeId { get; set; }
    public string CommonName { get; set; }
}