using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.Bankmaster.Commands.CreateBankmaster;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Commands.CreateBankDetail
{
    public class CreateBankDetailHandler(IBankdetailsRepository bankdetailsRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBankDetailCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateBankDetailCommand request,
            CancellationToken cancellationToken)
        {
            var allBanks = await bankdetailsRepository.GetAllAsync();

            if (allBanks.Any(x =>
                x.BankId.ToLower() == request.BankId.ToLower() &&
                x.Branch.ToLower() == request.Branch.ToLower()))
            {
                return BaseResult<string>.Failure("Bank already exists.");
            }



            var bankmasters = new Bankdetails(request.BankId, request.IFSCCode, request.Branch, request.Address, request.City1, request.City2, request.StateId, request.STDCode, request.PhNumber
)
            {
                Id = Ulid.NewUlid().ToString()
            };

            await bankdetailsRepository.AddAsync(bankmasters);
            await unitOfWork.SaveChangesAsync();

            return bankmasters.Id;
        }
    }
}
