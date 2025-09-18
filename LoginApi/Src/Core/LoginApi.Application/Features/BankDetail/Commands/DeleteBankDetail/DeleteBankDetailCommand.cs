using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Commands.DeleteBankDetail
{
    public class DeleteBankDetailCommand : IRequest<BaseResult>
    {
        public string Id { get; set; }
    }
}
