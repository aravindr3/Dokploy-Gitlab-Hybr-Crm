using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.TaskMaster.Command.Create;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.DomainStages.Command.Create
{
    public class CreateDomainStagesCommandHandler(
        IDomainStagesServices domainStagesServices,
       IStagesServices stagesServices,
        IDomainRepository domainRepository,
        ISubDomainServices subDomainServices,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDomainStagesCommand, BaseResult<string>>
    {
        public async Task<BaseResult<string>> Handle(CreateDomainStagesCommand request,
       CancellationToken cancellationToken)
        {
            var domainResult = await subDomainServices?.GetByIdAsync(request.SubDomainId);
            if (domainResult == null)
            {

                return new Error(ErrorCode.BadRequest, "subDomain not found");
            }
            //var domainId = domainResult?.Data?.Id;
            var parentId = request.ParentId;
            if (parentId == "")
            {
                parentId = "01JYJY1J0T6247H5ZCXCESYSX2";
            }
            var master = new Domain.Exchange.Entities.DomainStages(
            request?.SubDomainId,
             request.StagesId,
             request?.TemplateStatus,
             request?.TemplateId,
             parentId
          


                );
            master.Id = Ulid.NewUlid().ToString();
            await domainStagesServices.AddAsync(master);
            await unitOfWork.SaveChangesAsync();

            return master.Id;
        }

    }
   
}
