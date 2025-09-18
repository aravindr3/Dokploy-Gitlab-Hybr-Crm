using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Features.DomainStages.Queries.GetAll;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.SubDomain.Queries.GetAll
{
    public class GetAllSubDomainQueryHandler(IDomainStagesServices domainStagesServices, IStagesServices stagesServices, IDomainRepository domainRepository,
        ISubDomainServices subDomainServices)
    : IRequestHandler<GetAllSubDomainquery, BaseResult<List<SubDomainDto>>>
    {
        public async Task<BaseResult<List<SubDomainDto>>> Handle(GetAllSubDomainquery request, CancellationToken cancellationToken)
        {
            var masters = await subDomainServices.GetAllAsync();
            var masterList = masters.Where(a => a.Status == 1).ToList();

            if (!masterList.Any()) return new Error(ErrorCode.NotFound, "SubDomain not found");

            var dtoList = new List<SubDomainDto>();

            foreach (var master in masterList)
            {
                var domain = domainRepository?.GetDomainById(master?.DomainId)?.Result?.Data.DomainName;
                var dto = new SubDomainDto
                {
                    Id = master.Id,
                    DomainId =domainRepository?.GetDomainById(master.DomainId).Result?.Data?.Id,
                    SubDomainName = master?.Id,
                    DomainName = domain,
                    CategoryName = master?.CategoryName,
                    Status = master.Status

                };
                dtoList.Add(dto);
            }

            return BaseResult<List<SubDomainDto>>.Ok(dtoList);
        }

    
    }
}
