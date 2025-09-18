using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.CommonTypeMasters.Queries.GetStateById
{
    public class GetStateByIdQuery : IRequest <BaseResult<StateMasterDto>>
    {
        public string Id { get; set; }
    }
}
