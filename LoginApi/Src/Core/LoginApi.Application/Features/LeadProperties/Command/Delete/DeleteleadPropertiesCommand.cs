using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Command.Delete
{
    public class DeleteleadPropertiesCommand : IRequest<BaseResult>
    {
        public string ? LeadId { get; set; }
    }
}
