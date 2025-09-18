using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Queries.GetAllCommonMaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.StateMaster.Queries
{
    public class GetAllStateMasterQueryHandler(IStateMaster stateMaster)
    : IRequestHandler<GetAllstateQuery, BaseResult<List<StateMasterDto>>>
    {
        public async Task<BaseResult<List<StateMasterDto>>> Handle(GetAllstateQuery request,
            CancellationToken cancellationToken)
        {
            var states = await stateMaster.GetAllWithChildAsync(o => o.Country );
            var state = states.Where(a => a.Status == 1);
            if (state is null) return new Error(ErrorCode.NotFound, "state not found");

            var statemastedto = state
            .Select(c => new StateMasterDto(c))
            .ToList();
            return BaseResult<List<StateMasterDto>>.Ok(statemastedto);

        }

    }
}
