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

namespace HyBrForex.Application.Features.BankDetail.Queries.GetBankDetailsById
{
    public class GetBankdetailsByIdHandler(
         IBankdetailsRepository bankdetailsRepository
     ) : IRequestHandler<GetBankdetailsByIdQuery, BaseResult<BankdetailsDto>>
    {
        public async Task<BaseResult<BankdetailsDto>> Handle(GetBankdetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var bankDetail = await bankdetailsRepository
                .GetAllWithChildAsync(x => x.Bank, x => x.State); // this method seems to return all — we need GetById with includes

            var entity = bankDetail
                .FirstOrDefault(b => b.Id == request.Id && b.Status == 1);

            if (entity is null)
                return new Error(ErrorCode.NotFound, "Bank Detail not found");

            var dto = new BankdetailsDto
            {
                Id = entity.Id,
                BankId = entity.BankId,
                BankName = entity.Bank?.BankName,
                IFSCCode = entity.IFSCCode,
                Branch = entity.Branch,
                Address = entity.Address,
                City1 = entity.City1,
                City2 = entity.City2,
                StateId = entity.StateId,
                StateName = entity.State?.StateName,
                STDCode = entity.STDCode,
                PhNumber = entity.PhNumber,
                Status = entity.Status
            };

            return BaseResult<BankdetailsDto>.Ok(dto);
        }
    }
}
