using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.Exchange.DTOs;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Queries.GetAllBankDetails
{
    public class GetAllBankDetailsQuery : IRequest<BaseResult<List<BankdetailsDto>>>
    {
    }
}
