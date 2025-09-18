using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;

public class GetCommonMasterByIdQueryHandler(
    ICommonMasterRepository commonMasterRepository,
    ICommonTypeMsaterRepository commonTypeMsaterRepository
    ) : IRequestHandler<GetCommonMasterByIdQuery, BaseResult<CommonMasterDto>>
{
    public async Task<BaseResult<CommonMasterDto>> Handle(GetCommonMasterByIdQuery request,
        CancellationToken cancellationToken)
    {
        var master = await commonMasterRepository.GetByIdAsync(request.Id);
       
        if (master is null || master.Status == 0 )
            return new Error(ErrorCode.NotFound, "CommonMaster not found", nameof(request.Id));
        var typeName = commonTypeMsaterRepository.GetByIdAsync(master.CommonTypeId)?.Result?.TypeName?.ToString();
        return new CommonMasterDto(master , typeName);
    }
}