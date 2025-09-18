using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetCommonTypeMasterById;

public class GetCommonTypeMasterByIdQueryHandler(
    ICommonTypeMsaterRepository commonTypeMsaterRepository) : IRequestHandler<GetCommonTypeMasterByIdQuery, BaseResult<CommonTypeMsaterDto>>
{
    public async Task<BaseResult<CommonTypeMsaterDto>> Handle(GetCommonTypeMasterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var commonTypeMsaters = await commonTypeMsaterRepository.GetByIdChildAsync(a => a.Id == request.Id && a.Status == 1 );
       var commonTypeMsater = commonTypeMsaters.FirstOrDefault();
        if (commonTypeMsater is null)
            return new Error(ErrorCode.NotFound, "Common type master is not found", nameof(request.Id));

        return new CommonTypeMsaterDto(commonTypeMsater);
    }
}