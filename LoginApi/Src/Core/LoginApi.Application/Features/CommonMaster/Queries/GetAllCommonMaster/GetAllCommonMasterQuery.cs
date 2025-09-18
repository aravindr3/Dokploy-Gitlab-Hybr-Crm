using System.Collections.Generic;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;

public class GetAllCommonMasterQuery : IRequest<BaseResult<List<CommonMasterDto>>>
{
}