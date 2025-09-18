using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class ActivityLog : AuditableBaseEntity
    {
        public ActivityLog()
        {
        }

        public ActivityLog(string? leadId , string ? ownerId , string ? activityTypeId , string description, 
            DateTime activityDate , string ? notes , string ? callType)
        {
            LeadId = leadId;
            OwnerId = ownerId;
            ActivityTypeId = activityTypeId;
            Description = description;
            ActivityDate = activityDate;
            Notes = notes;
            CallType = callType;
        }

        public string ? LeadId { get; set; }
        public string ? OwnerId { get; set; }
        public string ? ActivityTypeId { get; set; }
        public string ? Description { get; set; }
        public DateTime ? ActivityDate { get; set; }
        public string ? Notes { get; set; }
        public string ? CallType { get; set; }
    }
}
