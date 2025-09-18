using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.StateMaster.Queries
{
    public class GetAllstateQuery : IRequest<BaseResult<List<StateMasterDto>>>
    {
    }
}
