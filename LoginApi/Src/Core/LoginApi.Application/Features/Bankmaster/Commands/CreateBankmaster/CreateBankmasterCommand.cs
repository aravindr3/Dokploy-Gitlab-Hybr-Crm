using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Commands.CreateBankmaster
{
    public class CreateBankmasterCommand : IRequest<BaseResult<string>>
    {
        public string BankName {  get; set; }
        public string BankCode { get; set; }
    }
}
