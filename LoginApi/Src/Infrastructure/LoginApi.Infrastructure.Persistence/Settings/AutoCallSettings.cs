using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrCRM.Application.Interfaces.Repositories;

namespace HyBrCRM.Infrastructure.Persistence.Settings
{
    public class AutoCallSettings : IAutoCallSettings
    {
        public string AutocallType { get; set; }
        public string RingStrategy { get; set; }
        public string LegACallerID { get; set; }
        public string LegAChannelID { get; set; }
        public string LegADialAttempts { get; set; }
        public string LegBCallerID { get; set; }
        public string LegBChannelID { get; set; }
        public string LegBDialAttempts { get; set; }
    }
}
