using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;

public class GetAllCommonMasterQueryHandler(ICommonMasterRepository commonMasterRepository)
    : IRequestHandler<GetAllCommonMasterQuery, BaseResult<List<CommonMasterDto>>>
{
    public async Task<BaseResult<List<CommonMasterDto>>> Handle(GetAllCommonMasterQuery request,
        CancellationToken cancellationToken)
    {
        var master = await commonMasterRepository.GetAllWithChildAsync(o => o.CommonTypeMsater);

        if (master is null) return new Error(ErrorCode.NotFound, "CommonMaster not found");

        var dtoList = master.Select(commonMaster => new CommonMasterDto
        {
            Id = commonMaster.Id,
            CommonName = commonMaster.CommonName,
            CommonType = commonMaster.CommonTypeMsater?.TypeName!,
            CommonTypeId = commonMaster.CommonTypeId,
            CreatedDateTime = Convert.ToDateTime(commonMaster.Created),
            Status = commonMaster.Status
        }).Where(a => a.Status == 1).ToList();
        return BaseResult<List<CommonMasterDto>>.Ok(dtoList);
    }
}