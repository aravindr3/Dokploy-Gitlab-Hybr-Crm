using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetCommonTypeMasterById;

public class GetCommonTypeMasterByIdQuery : IRequest<BaseResult<CommonTypeMsaterDto>>
{
    public string Id { get; set; }
}