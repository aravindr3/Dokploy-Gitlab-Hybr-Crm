using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadDocument.Command.Delete
{
    public class DeleteLeadDocumentCommand : IRequest<BaseResult>
    {
        public string ? Id { get; set; }
    }
}
