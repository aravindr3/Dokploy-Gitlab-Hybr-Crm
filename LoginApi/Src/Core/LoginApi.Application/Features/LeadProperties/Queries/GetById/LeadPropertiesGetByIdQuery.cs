using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Domain.Exchange.DTOs;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Queries.GetById
{
    public class LeadPropertiesGetByIdQuery : IRequest<BaseResult<List<LeadPropertyValueDto>>>
    {
        public string? LeadId { get; set; }
        public string ? DomainId { get; set; }
    }
}
