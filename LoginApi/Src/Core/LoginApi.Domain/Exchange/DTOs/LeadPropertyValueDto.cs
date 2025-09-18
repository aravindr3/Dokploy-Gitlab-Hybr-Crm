using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadPropertyValueDto
    {
        public LeadPropertyValueDto()
        {
        }

        public LeadPropertyValueDto(LeadProperyValue leadProperyValue)
        {
            Id = leadProperyValue.Id;
            LeadId = leadProperyValue.LeadId;
            OwnerId = leadProperyValue.OwnerId;
            PropertyDefinitionId = leadProperyValue.PropertyDefinitionId;
            Value = leadProperyValue.Value;
            Status = leadProperyValue.Status;
        }
        public string ? Id { get; set; }
        public string? LeadId { get; set; }
        public string ? LeadName { get; set; }
        public string? OwnerId { get; set; }
        public string ? OwnerName {  get; set; }
        public string? PropertyDefinitionId { get; set; }
        public string ? PropertyDefinitionName { get; set; }
        public string ? PropertyDefinitionDisplayName { get; set; }
        public string? Value { get; set; }
        public int ? Status { get; set; }
    }
}
