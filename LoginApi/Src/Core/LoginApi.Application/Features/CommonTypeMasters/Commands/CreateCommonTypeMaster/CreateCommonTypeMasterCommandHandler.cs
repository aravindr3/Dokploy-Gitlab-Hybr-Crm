using System;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Commands.CreateCommonTypeMaster;

public class CreateCommonTypeMasterCommandHandler(
    ICommonTypeMsaterRepository commonTypeMasterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCommonTypeMasterCommand, UlidBaseResult<string>>
{
    public async Task<UlidBaseResult<string>> Handle(CreateCommonTypeMasterCommand request,
        CancellationToken cancellationToken)
    {
        var product = new CommonTypeMsater(request.TypeName);
        product.Id = Ulid.NewUlid().ToString();

        try
        {
            await commonTypeMasterRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
        }

        return product.Id;
    }
}