using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.Bankmaster.Queries.GetAllBanks;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Queries.GetAllBankDetails
{
    public class GetAllBankDetailsHandler(IBankdetailsRepository bankdetailsRepository)
    : IRequestHandler<GetAllBankDetailsQuery, BaseResult<List<BankdetailsDto>>>
    {
        public async Task<BaseResult<List<BankdetailsDto>>> Handle(GetAllBankDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var purchase = await bankdetailsRepository.GetAllWithChildAsync(x => x.Bank, w => w.State);

            if (purchase is null) return new Error(ErrorCode.NotFound, "BankDetails not found");

            var dtoList = purchase.Select(c => new BankdetailsDto
            {
                Id = c.Id,
                BankId = c?.BankId,
                BankName = c.Bank.BankName,
                IFSCCode = c.IFSCCode,
                Branch = c.Branch,
                Address = c.Address,
                City1 = c.City1,
                City2 = c.City2,
                StateId = c.StateId,
                StateName = c.State.StateName,
                STDCode = c.STDCode,
                PhNumber = c.PhNumber,
                Status = c.Status,



            }).Where(a => a.Status == 1).ToList();
            return BaseResult<List<BankdetailsDto>>.Ok(dtoList);
        }
    }
}

