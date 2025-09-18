using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;

namespace HyBrCRM.Application.Interfaces.Repositories
{
    public interface IBonvoiceSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
