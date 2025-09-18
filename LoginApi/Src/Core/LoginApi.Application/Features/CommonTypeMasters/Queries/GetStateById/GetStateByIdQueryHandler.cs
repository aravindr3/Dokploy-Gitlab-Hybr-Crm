using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Queries.GetCommonMasterById;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetStateById
{
    public class GetStateByIdQueryHandler(


        IStateMaster stateMaster) : IRequestHandler<GetStateByIdQuery , BaseResult<StateMasterDto>>
{
    public async Task<BaseResult<StateMasterDto>> Handle(GetStateByIdQuery request,
        CancellationToken cancellationToken)
    {
        var state = await stateMaster.GetByIdAsync(request.Id);

        if (state is null || state.Status == 0)
            return new Error(ErrorCode.NotFound, "State not found", nameof(request.Id));

        return new StateMasterDto(state);
    }
}
}
