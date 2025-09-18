using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.TaskMaster.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.DomainStages.Queries.GetAll
{
    public class GetAllDomainStagesQueriesHandler(IDomainStagesServices domainStagesServices,IStagesServices stagesServices , IDomainRepository domainRepository,
        ISubDomainServices subDomainServices)
    : IRequestHandler<GetAllDomainStages, BaseResult<List<DomainStagesDto>>>
    {
        public async Task<BaseResult<List<DomainStagesDto>>> Handle(GetAllDomainStages request, CancellationToken cancellationToken)
        {
            var masters = await domainStagesServices.GetAllAsync();
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any()) return new Error(ErrorCode.NotFound, "Domain Stages not found");

            var dtoList = new List<DomainStagesDto>();
            var parentId = "";
            foreach (var master in masterList)
            {
                var domain =  subDomainServices?.GetByIdAsync(master?.DomainId)?.Result?.CategoryName;
                var stages = stagesServices?.GetByIdAsync(master?.StagesId)?.Result?.Name;
                var parentstages = domainStagesServices?.GetByIdAsync(master?.ParentId)?.Result?.StagesId;
                var parentName = "";
                if (parentstages != "01JYJY1J0T6247H5ZCXCESYSX2")
                {
                     parentName = stagesServices?.GetByIdAsync(parentstages)?.Result?.Name;
                }
                parentId = master?.ParentId;
                if(parentId == "01JYJY1J0T6247H5ZCXCESYSX2")
                {
                    parentId = "";
                }
                

                var dto = new DomainStagesDto
                {
                    Id = master.Id,
                    DomainId = master?.DomainId,
                    StagesId = master?.StagesId,
                    DomainStagesName = stages,
                    DomainName = domain,
                    TemplateStatus = master?.TemplateStatus,
                    TemplateId = master?.TemplateId,
                    ParentId = parentId,
                    ParentName = parentName,

                    Status = master?.Status

                };
                dtoList.Add(dto);
            }

            return BaseResult<List<DomainStagesDto>>.Ok(dtoList);
        }

    
    }
}
