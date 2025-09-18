using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.SubDomain.Command.Create
{
    public class CreateSubDomainCommand : IRequest<BaseResult<string>>
    {
        public string? DomainId { get; set; }
        public string? CategoryName { get; set; }

    }
  
}
