using System.Collections.Generic;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class CommonTypeMsater : AuditableBaseEntity
    {
        public CommonTypeMsater()
        {
        }

        public CommonTypeMsater(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
        public virtual ICollection<CommonMsater>? CommonMsater { get; set; }

        public void Update(string typeName)
        {
            TypeName = typeName;
        }
    }
}