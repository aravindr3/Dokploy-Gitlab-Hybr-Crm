using System;
using System.Threading;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.Entities;
using MediatR;

namespace HyBrForex.Application.Features.CommonMaster.Commands.CreateCommonMaster;

public class CreateCommonMasterCommandHandler(
    ICommonMasterRepository commonMasterRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCommonMasterCommand, UlidBaseResult<string>>
{
    public async Task<UlidBaseResult<string>> Handle(CreateCommonMasterCommand request,
        CancellationToken cancellationToken)
    {
        var master = new CommonMsater(request.CommonTypeId,
            request.CommonName);
        master.Id = Ulid.NewUlid().ToString();
        await commonMasterRepository.AddAsync(master);
        await unitOfWork.SaveChangesAsync();

        return master.Id;
    }
}