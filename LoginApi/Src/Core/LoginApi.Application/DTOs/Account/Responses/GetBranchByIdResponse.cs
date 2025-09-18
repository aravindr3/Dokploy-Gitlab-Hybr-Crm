using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Account.Responses
{
    public class GetBranchByIdResponse
    {
        public string BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
    }

}
