using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.BankDetail.Commands.CreateBankDetail
{
    public class CreateBankDetailCommand : IRequest<BaseResult<string>>
    {
        public string? BankId { get; set; }
        public string? IFSCCode { get; set; }
        public string? Branch { get; set; }

        public string? Address { get; set; }
        public string? City1 { get; set; }
        public string? City2 { get; set; }
        public string? StateId { get; set; }

        public string? STDCode { get; set; }
        public string? PhNumber { get; set; }
    }
}
