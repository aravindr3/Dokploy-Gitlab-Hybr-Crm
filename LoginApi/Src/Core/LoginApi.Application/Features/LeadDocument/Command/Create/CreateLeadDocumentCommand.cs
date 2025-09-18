using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HyBrCRM.Application.Features.LeadDocument.Command.Create
{
    public class CreateLeadDocumentCommand : IRequest<BaseResult<string>>
    {
        public string? LeadId { get; set; }
        public string? Remark { get; set; }

        public IFormFile? File { get; set; }
    }
}
