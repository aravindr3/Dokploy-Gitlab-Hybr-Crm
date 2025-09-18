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

namespace HyBrForex.Application.Features.Bankmaster.Queries.GetBankById
{
    public class GetBankByIdHandler(
    IBankmasterRepository bankmasterRepository) : IRequestHandler<GetBankByIdQuery, BaseResult<BankMasterDto>>
    {
        public async Task<BaseResult<BankMasterDto>> Handle(GetBankByIdQuery request,
            CancellationToken cancellationToken)
        {
            var master = await bankmasterRepository.GetByIdAsync(request.Id);

            if (master is null || master.Status == 0)
                return new Error(ErrorCode.NotFound, "Bank Master not found", nameof(request.Id));

            return new BankMasterDto(master);
        }

    }
}