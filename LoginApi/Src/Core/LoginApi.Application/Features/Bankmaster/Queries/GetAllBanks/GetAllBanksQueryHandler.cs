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
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Queries.GetAllBanks
{
    public class GetAllBanksQueryHandler(IBankmasterRepository bankmasterRepository)
    : IRequestHandler<GetAllBanksQuery, BaseResult<List<BankMasterDto>>>
    {
        public async Task<BaseResult<List<BankMasterDto>>> Handle(GetAllBanksQuery request,
            CancellationToken cancellationToken)
        {
            var commonTypeMsater = await bankmasterRepository.GetAllAsync();
            if (commonTypeMsater is null) return new Error(ErrorCode.NotFound, "Bankmaster not found");

            var commonTypeMasterDtos = commonTypeMsater
            .Where(c => c.Status == 1)  // Filter records with Status = 1
            .Select(c => new BankMasterDto(c))
            .ToList();

            return BaseResult<List<BankMasterDto>>.Ok(commonTypeMasterDtos);
        }
    }
}
