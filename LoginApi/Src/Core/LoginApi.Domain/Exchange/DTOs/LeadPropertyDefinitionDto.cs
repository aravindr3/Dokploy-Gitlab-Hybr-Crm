using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadPropertyDefinitionDto
    {
        public LeadPropertyDefinitionDto()
        {
        }

        public LeadPropertyDefinitionDto(LeadProperyDefinition leadProperyDefinition)
        {
            Id = leadProperyDefinition.Id;
            Domain = leadProperyDefinition.Domain;
            FieldName = leadProperyDefinition.FieldName;
            DisplayName = leadProperyDefinition.DisplayName;
            DataType = leadProperyDefinition.DataType;
            IsRequired = leadProperyDefinition.IsRequired;
            Status = leadProperyDefinition.Status;
        }

        public string ? Id { get; set; }
        public string? Domain { get; set; }
        public string ?DomainName { get; set; }
        public string? FieldName { get; set; }
        public string? DisplayName { get; set; }
        public string? DataType { get; set; }
        public bool? IsRequired { get; set; }
        public int ? Status { get; set; }
    }
}
