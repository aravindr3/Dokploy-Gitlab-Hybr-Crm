using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.StateMaster.Command
{
    public class CreateStateMastercommand :IRequest<BaseResult<String>>
    {
        public string? StateCode { get; set; }
        public string? StateName { get; set; }
        public int? GSTCode { get; set; }
        public string? ConuntryId { get; set; }
    }
}
