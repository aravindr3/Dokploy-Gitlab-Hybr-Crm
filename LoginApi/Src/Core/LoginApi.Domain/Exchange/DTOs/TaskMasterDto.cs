using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class TaskMasterDto
    {
        public TaskMasterDto()
        {
        }

        public TaskMasterDto(TaskMaster taskMaster)
        {
            Id = taskMaster.Id;
            LeadId = taskMaster.LeadId;
            OwnerId = taskMaster.OwnerId;
            DomainStagesId = taskMaster.DomainStagesId;
            TaskDate = taskMaster.TaskDate;
            TaskNote = taskMaster.TaskNote;
            TaskStatus = taskMaster.TaskStatus;
            //CountryInterestedIn = taskMaster.CountryInterestedIn;
            UniversityPreferred = taskMaster.UniversityPreferred;
            SubDescription = taskMaster.SubDescription;
            DepositPaidUniversity = taskMaster.DepositPaidUniversity;
            CallType = taskMaster?.CallType;
            Status = taskMaster.Status;
           
           
            
    }
        public string ? Id { get; set; }
        public string? LeadId { get; set; }
        public string ? LeadName { get; set; }
        public string? OwnerId { get; set; }
        public string ? OwnerName { get; set; }
        public string? DomainStagesId { get; set; }
        public string ? DomainName { get; set; }
        public string ? StagesName { get; set; }
        public string ? DomainStagesName { get; set; }
        //public string? CountryInterestedIn { get; set; }
        //public string ? CountryName { get; set; }
        public List<string>? CountryInterestedIn { get; set; }  // Country IDs
        public List<string>? CountryName { get; set; }
        public string? UniversityPreferred { get; set; }
        public string? SubDescription { get; set; }
        public string ? SubDescriptionName { get; set; }
        public string? DepositPaidUniversity { get; set; }
        public DateTime? TaskDate { get; set; }
        public string? TaskNote { get; set; }
        public string ? TaskStatus { get; set; }
        public int ? Status { get; set; }
        public string ? CallType { get; set; }

        public LeadDto? Lead { get; set; }
        public HoliDayLeadDto ? HolidayLead { get; set; }
        public List<LeadDocumentDto>? LeadDocument { get; set; }


    }
}
