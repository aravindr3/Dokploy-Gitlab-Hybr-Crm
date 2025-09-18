using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Command.Create
{
    public class CreateLeadCommand : IRequest<BaseResult<string>>
    {
        public string? LeadContactId { get; set; }
        public string? LeadSourceId { get; set; }
        public string? LeadStatusId { get; set; }
        public string? VerticalId { get; set; }
        public string? Notes { get; set; }
        public string ? CategoryId { get; set; }
        public string ? PreferedCountry {  get; set; }
        public string ? OwnerId { get; set; }
        public string ? DomainId { get; set; }
    }
}
