using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBrCRM.Application.Interfaces.Repositories
{
    public interface IAutoCallSettings
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
