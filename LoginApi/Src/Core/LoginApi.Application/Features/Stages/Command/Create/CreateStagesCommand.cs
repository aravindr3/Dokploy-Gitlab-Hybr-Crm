using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Stages.Command.Create
{
    public class CreateStagesCommand : IRequest<BaseResult<string>>
    {
        public string ? Name { get; set; }
    }
}
