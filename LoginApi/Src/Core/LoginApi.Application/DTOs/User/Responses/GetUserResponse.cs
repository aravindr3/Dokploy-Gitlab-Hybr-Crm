using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.User.Responses
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VerticalId { get; set; }
        public string ? VerticalName { get; set; }
        public string ? DomainId { get; set; }
        public string ? DomainName { get; set; }
        public List<string> Roles { get; set; }
        public List<string> BranchNames { get; set; }
    }

}
