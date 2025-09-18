using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Application.Interfaces.UserInterfaces;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrForex.Application.Features.BankDetail.Queries.GetAllBankDetails;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperyDefinition.GetAll
{
    public class GetAllLeadPropertyDefinitionQueryHandler(IleadPropertyDefinitionServices leadPropertyDefinitionServices,
        IDomainRepository domainRepository )
    : IRequestHandler<GetAllLeadProperyDefinitionQuery, BaseResult<List<LeadPropertyDefinitionDto>>>
    {
        public async Task<BaseResult<List<LeadPropertyDefinitionDto>>> Handle(GetAllLeadProperyDefinitionQuery request,
            CancellationToken cancellationToken)
        {
            var purchase = await leadPropertyDefinitionServices.GetAllAsync();

            if (purchase is null) return new Error(ErrorCode.NotFound, "LeadProperyDefinition not found");

            var dtoList = purchase.Select(c => new LeadPropertyDefinitionDto
            {
                Id = c.Id,
                Domain = c?.Domain,
                DomainName = domainRepository.GetDomainById(c.Domain)?.Result?.Data?.DomainName,
                DisplayName = c.DisplayName,
                FieldName = c.FieldName,
                DataType = c.DataType,
                IsRequired = c.IsRequired,
               
                Status = c.Status,

               



            }).Where(a => a.Status == 1).ToList();
            return BaseResult<List<LeadPropertyDefinitionDto>>.Ok(dtoList);
        }
    }
   
}
