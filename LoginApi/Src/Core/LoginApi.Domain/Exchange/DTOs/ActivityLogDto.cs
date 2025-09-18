using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class ActivityLogDto
    {
        public ActivityLogDto()
        {
        }

        public ActivityLogDto(ActivityLog activityLog)
        {
            Id = activityLog.Id;
            LeadId = activityLog.LeadId;
            OwnerId = activityLog.OwnerId;
            ActivityTypeId = activityLog.ActivityTypeId;
            Description = activityLog.Description;
            ActivityDate = activityLog.ActivityDate;
            CallType = activityLog?.CallType;
            Notes = activityLog?.Notes;
            Status = activityLog.Status;
        }

        public string ? Id { get; set; }
        public string? LeadId { get; set; }
        public string ? LeadName { get; set; }
        public string? OwnerId { get; set; }
        public string ? OwnerName { get; set; }
        public string? ActivityTypeId { get; set; }
        public string ? ActivityTypeName { get; set; }
        public string? Description { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string ? CallType { get; set; }
        public string ? Notes { get; set; }
        public int ? Status { get; set; }
    }
}
