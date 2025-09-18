using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;

public class GetCommonMasterByIdQuery : IRequest<BaseResult<CommonMasterDto>>
{
    public string Id { get; set; }
}