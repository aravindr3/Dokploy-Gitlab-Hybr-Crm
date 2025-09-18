using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class LeadProperyDefinition : AuditableBaseEntity
    {
        public LeadProperyDefinition()
        {
        }

       

        public LeadProperyDefinition(string? domain, string? fieldName, string? displayName, 
            string? dataType, bool? isRequired)
        {
            Domain = domain;
            FieldName = fieldName;
            DisplayName = displayName;
            DataType = dataType;
            IsRequired = isRequired;
        }

        public string ? Domain { get; set; }          
        public string ? FieldName { get; set; }        
        public string ? DisplayName { get; set; }
        public string ? DataType { get; set; }         
        public bool ? IsRequired { get; set; }
    }
}
