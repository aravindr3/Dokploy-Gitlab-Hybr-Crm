using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;
using HyBrForex.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class Lead : AuditableBaseEntity
    {
        public Lead(string ? leadContactId , string? leadSourceId ,string? leadStatusId, string vericalId ,
            string ? notes , string ? categoryId , string ? preferedCountry)
        {
            LeadContactId = leadContactId;
            LeadSourceId = leadSourceId;
            LeadStatusId = leadStatusId;
            VericalId = vericalId;
            Notes = notes;
            CategoryId = categoryId;
            PreferedCountry = preferedCountry;
        }

        public string ? LeadContactId { get; set; }
        public string ? LeadSourceId { get; set; }
        public string ? LeadStatusId { get; set; }
        public string ? VericalId { get; set; }
        public string ? CategoryId { get; set; }
        public string ? Notes {  get; set; }
        public string ? PreferedCountry {  get; set; }

        public virtual LeadContact ? LeadContact { get; set; }
        public virtual CommonMsater ? LeadSource { get; set; }
        public virtual DomainStages ? LeadStatus { get; set; }

        public void Update (string ?  leadContactId , string ? leadSourceId , string ? leadStatusId , string ? vericalId ,
            string ? notes, string ? categoryId , string ? preferedCountry)
        {
            LeadContactId = leadContactId;
            LeadSourceId = leadSourceId;
            LeadStatusId = leadStatusId;
            VericalId = vericalId;
            Notes = notes;
            CategoryId = categoryId;
            PreferedCountry = preferedCountry;
        }
    }
}
