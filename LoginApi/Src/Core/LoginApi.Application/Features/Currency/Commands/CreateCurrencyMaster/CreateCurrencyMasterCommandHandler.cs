using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

//using HyBrForex.Application.Features.Products.Commands.CreateProduct;

namespace HyBrForex.Application.Features.Currency.Commands.CreateCurrencyMaster;

public class CreateCurrencyMasterCommandHandler(ICurrencyRepository currencyRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCurrencyMasterCommand, BaseResult<string>>
{
    public async Task<BaseResult<string>> Handle(CreateCurrencyMasterCommand request,
        CancellationToken cancellationToken)
    {
        if (currencyRepository.GetAllAsync().Result
            .Any(a => a.ISD.Equals(request.ISD, StringComparison.CurrentCultureIgnoreCase)))
            return new Error(ErrorCode.AlreadyExists, "Currency Already Exists With Same ISD", nameof(request.ISD));

        var currencyMaster = new CurrencyMaster(request.CountryId, request.ISD, request.Currency, request.FLM8Cd)
        {
            Id = Ulid.NewUlid().ToString()
        };
        await currencyRepository.AddAsync(currencyMaster);
        await unitOfWork.SaveChangesAsync();
        return currencyMaster.Id;
    }
}