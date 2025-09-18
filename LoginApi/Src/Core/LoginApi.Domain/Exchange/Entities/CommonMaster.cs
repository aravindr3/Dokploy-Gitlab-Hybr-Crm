using System.Collections.Generic;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class CommonMsater : AuditableBaseEntity

    {
      

        public CommonMsater(string commonTypeId, string commonName)
        {
            CommonTypeId = commonTypeId;
            CommonName = commonName;
        }

        public string CommonTypeId { get; set; }
        public string CommonName { get; set; }
        public virtual CommonTypeMsater? CommonTypeMsater { get; set; } 
        public virtual ICollection<EmployeeMaster>? EmployeeMaster { get; set; }


        public void Update(string commonTypeId, string commonName)
        {
            CommonName = commonName;
            CommonTypeId = commonTypeId;
        }
    }
}