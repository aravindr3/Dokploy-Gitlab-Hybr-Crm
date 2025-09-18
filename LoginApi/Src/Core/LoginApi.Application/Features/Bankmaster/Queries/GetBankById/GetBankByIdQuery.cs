using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Queries.GetBankById
{
    public class GetBankByIdQuery : IRequest<BaseResult<BankMasterDto>>
    {
        public string Id { get; set; }
    }
}
   