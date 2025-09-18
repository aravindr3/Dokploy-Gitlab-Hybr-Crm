using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Features.StateMaster.Queries;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrCRM.Application.Features.Stages.Queries.GetAll
{
    public class GetAllStagesQueryHandler(IStagesServices stagesServices)
    : IRequestHandler<GetAllStagesQuery, BaseResult<List<StagesDto>>>
    {
        public async Task<BaseResult<List<StagesDto>>> Handle(GetAllStagesQuery request,
            CancellationToken cancellationToken)
        {
            var state = await stagesServices.GetAllAsync();

            if (state is null) return new Error(ErrorCode.NotFound, "Stages not found");

            var stageMasterDto = state
            .Select(c => new StagesDto(c))
            .ToList();
            return BaseResult<List<StagesDto>>.Ok(stageMasterDto);

        }

    }
   
}
