using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetAllCommonTypeMasterQuery;

public class GetAllCommonTypeMasterQueryHandler(
    ICommonTypeMsaterRepository commonTypeMsaterRepository) : IRequestHandler<GetAllCommonTypeMasterQuery, BaseResult<List<CommonTypeMsaterDto>>>
{
    public async Task<BaseResult<List<CommonTypeMsaterDto>>> Handle(GetAllCommonTypeMasterQuery request,
        CancellationToken cancellationToken)
    {
        var commonTypeMsaters = await commonTypeMsaterRepository.GetAllAsync();
        var commonTypeMsater = commonTypeMsaters.Where(a => a.Status == 1);
        if (commonTypeMsater is null) return new Error(ErrorCode.NotFound, "Common type master is not found");

        var commonTypeMasterDtos = commonTypeMsater
            .Select(c => new CommonTypeMsaterDto(c))
            .ToList();

        return BaseResult<List<CommonTypeMsaterDto>>.Ok(commonTypeMasterDtos);
    }
}