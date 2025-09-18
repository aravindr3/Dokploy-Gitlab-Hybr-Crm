using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.ActivityLog.Query.GetByLeadId
{
    public class GetActivityLogByLeadIdQuery : IRequest<BaseResult<List<ActivityLogDto>>>
    {
        public string ? LeadId { get; set; }
        public string ? DomainId { get; set; }
       
    }
}
