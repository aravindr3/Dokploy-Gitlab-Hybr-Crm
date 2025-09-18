using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.DomainStages.Command.Create
{
    public class CreateDomainStagesCommand :IRequest<BaseResult<string>>
    {
        public string? SubDomainId { get; set; }
        public string? StagesId { get; set; }
        public bool ? TemplateStatus {  get; set; }
        public string ? TemplateId { get; set; }
        public string ? ParentId { get; set; }

    }
}
