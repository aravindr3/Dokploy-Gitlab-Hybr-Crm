using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public  class Stages : AuditableBaseEntity
    {
        public Stages(string name )
        {
            Name = name;
        }

        public string ? Name { get; set; }
    }
}
