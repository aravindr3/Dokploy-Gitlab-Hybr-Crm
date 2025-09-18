using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class HoliDaysLead : AuditableBaseEntity
    {
        public HoliDaysLead()
        {
        }

        public HoliDaysLead(string? leadContactId, string? enquiryType, string? travelType, 
            string? prefferedDestination, string? tripDuration, string? depatureCity, DateTime? startDate,
            DateTime? endDate, int adults, int childWithBed, int childWithoutBed, string? beddingPreference,
            string? roomType, string? mealPlan, string? notes, DateTime? followUpDate, string? asignedAgent , string? verticalId ,
            string ? leadSourceId , string ? leadStatusId , string ? categoryId)
        {
            LeadContactId = leadContactId;
            EnquiryType = enquiryType;
            TravelType = travelType;
            PrefferedDestination = prefferedDestination;
            TripDuration = tripDuration;
            DepatureCity = depatureCity;
            StartDate = startDate;
            EndDate = endDate;
            Adults = adults;
            ChildWithBed = childWithBed;
            ChildWithoutBed = childWithoutBed;
            BeddingPreference = beddingPreference;
            RoomType = roomType;
            MealPlan = mealPlan;
            Notes = notes;
            FollowUpDate = followUpDate;
            AsignedAgent = asignedAgent;
            VerticalId = verticalId;
            LeadSourceId = leadSourceId;
            LeadStatusId = leadStatusId;
            CategoryId = categoryId;
        }

        public string ? LeadContactId { get; set; }
        public string ? EnquiryType { get; set; }
        public string ? TravelType { get; set; }
        public string ? PrefferedDestination { get; set; }
        public string ? TripDuration { get; set; }
        public string ? DepatureCity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Adults { get; set; }
        public int ChildWithBed {  get; set; }
        public int ChildWithoutBed { get; set; }
        public string ? BeddingPreference { get; set; }
        public string ? RoomType { get; set; }
        public string ? MealPlan { get; set; }
        public string ? Notes { get; set; }
        public DateTime ? FollowUpDate { get; set; }
        public string ? AsignedAgent {  get; set; }
        public string ? VerticalId { get; set; }
        public string? LeadSourceId { get; set; }
        public string? LeadStatusId { get; set; }
        public string ? CategoryId { get; set; }

        public void Update(string leadContactId, string enquiryType, string travelType, string prefferedDestination, string tripDuration, string depatureCity, DateTime? startDate, DateTime? endDate, int adults, int childWithBed, int childWithoutBed, 
            string beddingPreference, string roomType, string mealPlan, string notes, DateTime? followUpDate, string asignedAgent, string verticalId,
            string ? leadSourceId , string ? leadStatusId , string? categoryId)
        {
            LeadContactId = leadContactId;
            EnquiryType = enquiryType;
            TravelType = travelType;
            PrefferedDestination = prefferedDestination;
            TripDuration = tripDuration;
            DepatureCity = depatureCity;
            StartDate = startDate;
            EndDate = endDate;
            Adults = adults;
            ChildWithBed = childWithBed;
            ChildWithoutBed = childWithoutBed;
            BeddingPreference = beddingPreference;
            RoomType = roomType;
            MealPlan = mealPlan;
            Notes = notes;
            FollowUpDate = followUpDate;
            AsignedAgent = asignedAgent;
            VerticalId = verticalId;
            LeadSourceId = leadSourceId;
            LeadStatusId = leadStatusId;
            CategoryId = categoryId;
        }
    }
}
