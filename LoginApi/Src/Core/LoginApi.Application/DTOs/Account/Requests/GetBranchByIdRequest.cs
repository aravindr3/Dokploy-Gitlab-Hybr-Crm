using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.Account.Requests
{
    public class GetBranchByIdRequest
    {
        public List<string> BranchIds { get; set; } = new();
    }

}
