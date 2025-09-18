using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.UpdateCommonMaster;

public class UpdateCommonMasterCommand : IRequest<BaseResult>
{
    public string Id { get; set; }

    public string CommonTypeId { get; set; }
    public string CommonName { get; set; }
}