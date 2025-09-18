using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class LeadProperyValue : AuditableBaseEntity
    {
        public LeadProperyValue()
        {
        }

        public LeadProperyValue(string? leadId, string? ownerId, string? propertyDefinitionId, string? value)
        {
            LeadId = leadId;
            OwnerId = ownerId;
            PropertyDefinitionId = propertyDefinitionId;
            Value = value;
        }

        public string ? LeadId { get; set; }
        public string ? OwnerId { get; set; }
        public string ? PropertyDefinitionId { get; set; }
        public string ? Value { get; set; }
        public virtual LeadProperyDefinition ? PropertyDefinition { get; set; }
        public void Update(string ? leadId , string ? ownerId , string ? propertyDefinitionId , string ? value)
        {
            LeadId = leadId;
            OwnerId = ownerId;
            PropertyDefinitionId = propertyDefinitionId;
            Value = value;
        }
    }
}
