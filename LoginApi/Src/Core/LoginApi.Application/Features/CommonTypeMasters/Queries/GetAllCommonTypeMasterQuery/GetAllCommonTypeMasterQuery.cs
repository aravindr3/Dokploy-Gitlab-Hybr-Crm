using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetAllCommonTypeMasterQuery;

public class GetAllCommonTypeMasterQuery : IRequest<BaseResult<List<CommonTypeMsaterDto>>>
{
}