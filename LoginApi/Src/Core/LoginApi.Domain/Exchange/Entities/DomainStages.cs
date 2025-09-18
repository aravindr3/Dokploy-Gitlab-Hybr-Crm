using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class DomainStages : AuditableBaseEntity
    {
        public DomainStages(string domainId , string stagesId , bool ? templateStatus , string ? templateId , string ? parentId)
        {
            DomainId = domainId;
            StagesId = stagesId;
            TemplateStatus = templateStatus;
            TemplateId = templateId;
            ParentId = parentId;
        }

        public string ? DomainId { get; set; }
        public string ? StagesId { get; set; }
        public bool ? TemplateStatus { get; set; }
        public string ? TemplateId { get; set; }
        public string ? ParentId { get; set; }
        public virtual DomainStages? Parent { get; set; }

        public virtual Stages ? Stages { get; set; }
    }
}
