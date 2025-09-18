using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Lead.Command.Delete
{
    public class DeleteLeadCommand : IRequest<BaseResult>
    {
        public string ? Id { get; set; }
    }
}
