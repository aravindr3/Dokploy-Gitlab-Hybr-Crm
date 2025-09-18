using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class TaskMaster : AuditableBaseEntity
    {
        public TaskMaster()
        {
        }

        public TaskMaster(string leadId , string ownerId , string ? domainStagesId , DateTime ? taskDate , string ? taskNote ,
            string ? taskStatus ,string countryInterestedIn , string ? universityPrefered , string ? subDescription , string ? depositPaidUniversity,
            string ? callType 
            )
        {
            LeadId = leadId;
            OwnerId = ownerId;
            DomainStagesId = domainStagesId;
            TaskDate = taskDate;
            TaskNote = taskNote;
            TaskStatus = taskStatus;
            CountryInterestedIn = countryInterestedIn;
            UniversityPreferred = universityPrefered;
            SubDescription = subDescription;
            DepositPaidUniversity = depositPaidUniversity;
            CallType = callType;
        }

        public string ? LeadId { get; set; }
        public string ? OwnerId { get; set; }
        public string ? DomainStagesId { get; set; }
        public DateTime ? TaskDate { get; set; }
        public string ? CountryInterestedIn {  get; set; }
        public string ? UniversityPreferred { get; set; }
        public string ? SubDescription { get; set; }
        public string ? DepositPaidUniversity { get; set; }
        public string ? TaskNote { get; set; }
        public string ? TaskStatus { get; set; }
        public string ? CallType { get; set; }

        //public virtual Lead ? Lead { get; set; }
        public virtual DomainStages ? DomainStages { get; set; }

        public void Update(string leadId, string ownerId, string domainStagesId, DateTime? taskDate, string taskNote , string ? taskStatus ,
            string ? countryInterestedIn , string ? universityPrefered , 
            string ? subDescription ,string ? depositPaidtoUniversity,
            string ? callType)
        {
            LeadId = leadId;
            OwnerId = ownerId;
            DomainStagesId = domainStagesId;
            TaskDate = taskDate;
            TaskNote = taskNote;
            TaskStatus = taskStatus;
            CountryInterestedIn = countryInterestedIn;
            UniversityPreferred = universityPrefered;
            SubDescription = subDescription;
            DepositPaidUniversity = depositPaidtoUniversity;
            CallType = callType;
        }
    }
}
