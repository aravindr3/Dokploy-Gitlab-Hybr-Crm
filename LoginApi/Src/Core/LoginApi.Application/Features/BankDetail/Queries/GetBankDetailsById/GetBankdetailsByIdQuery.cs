using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Queries.GetBankDetailsById
{
    public class GetBankdetailsByIdQuery : IRequest<BaseResult<BankdetailsDto>>
    {
        public string Id { get; set; }
    }
}
