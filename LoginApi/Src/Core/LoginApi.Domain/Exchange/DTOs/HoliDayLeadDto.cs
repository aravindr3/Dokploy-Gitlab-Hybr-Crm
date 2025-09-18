using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class HoliDayLeadDto
    {
        public HoliDayLeadDto()
        {
        }

        public HoliDayLeadDto(HoliDaysLead holiDaysLead)
        {
            Id = holiDaysLead.Id;
            LeadContactId = holiDaysLead.LeadContactId;
            EnquiryType = holiDaysLead.EnquiryType;
            TravelType = holiDaysLead.TravelType;
            PrefferedDestination = holiDaysLead?.PrefferedDestination;
            TripDuration = holiDaysLead?.TripDuration;
            DepatureCity = holiDaysLead?.DepatureCity;
            StartDate = holiDaysLead?.StartDate;
            EndDate = holiDaysLead?.EndDate;
            Adults = holiDaysLead?.Adults ?? 0;
            ChildWithBed = holiDaysLead?.ChildWithBed ?? 0;
            ChildWithoutBed = holiDaysLead?.ChildWithoutBed ?? 0;
            BeddingPreference = holiDaysLead?.BeddingPreference;
            RoomType = holiDaysLead?.RoomType;
            MealPlan = holiDaysLead?.MealPlan;
            Notes = holiDaysLead?.Notes;
            FollowUpDate = holiDaysLead?.FollowUpDate;
            AsignedAgent = holiDaysLead?.AsignedAgent;
            VerticalId = holiDaysLead?.VerticalId;
            CategoryId = holiDaysLead?.CategoryId;
            Created = holiDaysLead?.Created;
            Status = holiDaysLead?.Status;

        }
        public string ? Id { get; set; }
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
        public string? VerticalId { get; set; }
        public string ? VerticalName { get; set; }
        public int ? Status { get; set; }
        public string ? LeadSourceId { get; set; }
        public string? LeadSourceName { get;set; }
        public string? LeadStatusId { get;set; }
        public string? LeadStatusName { get; set;}
        public string ? CategoryId { get; set; }
        public string ? CategoryName { get; set; }
     public DateTime ? Created {  get; set; }
        public DateTime ? LastTask {  get; set; }
        public string ? Stage {  get; set; }
        public LeadContactDto? Contact { get; set; }
        public List<LeadPropertyValueDto>? LeadProperties { get; set; }
        public List<LeadDocumentDto>? LeadDocument { get; set; }




    }
}
