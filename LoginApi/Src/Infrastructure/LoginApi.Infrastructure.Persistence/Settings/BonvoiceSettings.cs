using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;

namespace HyBrCRM.Infrastructure.Persistence.Settings
{
    public class BonvoiceSettings : IBonvoiceSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
