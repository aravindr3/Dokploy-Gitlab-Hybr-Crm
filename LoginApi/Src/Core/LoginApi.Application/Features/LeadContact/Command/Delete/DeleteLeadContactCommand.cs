using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadContact.Command.Delete
{
    public class DeleteLeadContactCommand: IRequest<BaseResult>
    {
        public string ? Id { get; set; }
    }
}
