using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.DomainStages.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.SubDomain.Command.Create
{
    public class CreateSubDomainCommandHandler(
        IDomainStagesServices domainStagesServices,
       IStagesServices stagesServices,
        IDomainRepository domainRepository,
        ISubDomainServices subDomainServices,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateSubDomainCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateSubDomainCommand request,
       CancellationToken cancellationToken)
        {
            var domainResult = await domainRepository?.GetDomainById(request.DomainId);
            if (domainResult == null)
            {

                return new Error(ErrorCode.BadRequest, "subDomain not found");
            }
            var domainId = domainResult?.Data?.Id;

            var master = new Domain.Exchange.Entities.SubDomain(
            domainId,
             request.CategoryName



                );
            master.Id = Ulid.NewUlid().ToString();
            await subDomainServices.AddAsync(master);
            await unitOfWork.SaveChangesAsync();

            return master.Id;
        }

    }
  
}
