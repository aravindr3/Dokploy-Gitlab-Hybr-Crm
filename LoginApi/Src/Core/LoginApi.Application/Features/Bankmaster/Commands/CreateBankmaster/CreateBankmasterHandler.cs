using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Commands.CreateBankmaster
{
    public class CreateBankmasterHandler(IBankmasterRepository bankmasterRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateBankmasterCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateBankmasterCommand request,
            CancellationToken cancellationToken)
        {
            var allBanks = await bankmasterRepository.GetAllAsync();

            if (allBanks.Any(x =>
                x.BankName.ToLower() == request.BankName.ToLower() ||
                x.BankCode.ToLower() == request.BankCode.ToLower()))
            {
                return BaseResult<string>.Failure("Bank with the same name and code already exists.");
            }



            var bankmasters = new Bankmasters(request.BankName, request.BankCode)
            {
                Id = Ulid.NewUlid().ToString()
            };

            await bankmasterRepository.AddAsync(bankmasters);
            await unitOfWork.SaveChangesAsync();

            return bankmasters.Id;
        }
    }
}