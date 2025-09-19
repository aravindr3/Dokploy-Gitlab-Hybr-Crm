using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.HolidaysLead.Command.Create
{
    public class CreateHolidayLeadCommand : IRequest<BaseResult<string>>
    {
        public string? LeadContactId { get; set; }
        public string? EnquiryType { get; set; }
        public string? TravelType { get; set; }
        public string? PrefferedDestination { get; set; }
        public string? TripDuration { get; set; }
        public string? DepatureCity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Adults { get; set; }
        public int ChildWithBed { get; set; }
        public int ChildWithoutBed { get; set; }
        public string? BeddingPreference { get; set; }
        public string? RoomType { get; set; }
        public string? MealPlan { get; set; }
        public string? Notes { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string? AsignedAgent { get; set; }
        public string ? VerticalId { get; set; }
        public string? LeadSourceId { get; set; }
        public string? LeadStatusId { get; set; }
        public string ? CategoryId { get; set; }
        public string ? OwnerId { get; set; }
        public string ? Other {  get; set; }
    }
}
