using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.Bankmaster.Commands.UpdateBankmaster
{
    public class UpdateBankmasterCommand : IRequest<BaseResult>
    {
        public string Id { get; set; }

        public string BankName { get; set; }
        public string BankCode { get; set; }
    }
   
}
