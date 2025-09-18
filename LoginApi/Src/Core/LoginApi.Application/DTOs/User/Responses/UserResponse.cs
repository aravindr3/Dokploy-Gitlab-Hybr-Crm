using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrForex.Application.DTOs.User.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string VerticalId { get; set; }
        public string VerticalName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public List<string> BranchIds { get; set; } = new List<string>();
    }

}
