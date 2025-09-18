using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.SubDomain.Queries.GetAll
{
    public class GetAllSubDomainquery : IRequest<BaseResult<List<SubDomainDto>>>
    {
    }
}
