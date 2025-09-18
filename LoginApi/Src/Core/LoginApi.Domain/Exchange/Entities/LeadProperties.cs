using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class LeadProperties : AuditableBaseEntity
    {
        public LeadProperties(string ownerId ,string leadId, string ? countryInterestedIn , bool ? documentUploadStatus ,
            string ? universityPreferred ,int? offerLetterStatus, bool? depositPaidUniversity , bool? visaStatus , int ? refundStatus,
           DateTime?  futureIntake
            )
        {
             OwnerId = ownerId;
            LeadId = leadId;
            CountryInterestedIn = countryInterestedIn;
            DocumentUploadStatus = documentUploadStatus;
            UniversityPreferred = universityPreferred;
            OfferLetterStatus = offerLetterStatus;
            DepositPaidUniversity = depositPaidUniversity;
            VisaStatus = visaStatus;
            RefundStatus = refundStatus;
        FutureIntake = futureIntake;
        }

        public string ? OwnerId { get; set; }
        public string ? LeadId { get; set; }
        public string ? CountryInterestedIn {  get; set; }
        public bool ? DocumentUploadStatus { get; set; }
        public string ? UniversityPreferred { get; set; }
        public int ? OfferLetterStatus { get; set; }
        public bool ? DepositPaidUniversity { get; set; }
        public bool ? VisaStatus {  get; set; }
        public int ? RefundStatus { get; set; }
        public DateTime ? FutureIntake { get; set; }

        public void Update(string ownerId, string leadId, 
            string countryInterestedIn, bool? documentUploadStatus, string universityPreferred,
            int? offerLetterStatus, bool? depositPaidUniversity, bool? visaStatus, int? refundStatus,
            DateTime? futureIntake)
        {
            OwnerId = ownerId;
            LeadId = leadId;
            CountryInterestedIn = countryInterestedIn;
            DocumentUploadStatus = documentUploadStatus;
            UniversityPreferred = universityPreferred;
            OfferLetterStatus = offerLetterStatus;
            DepositPaidUniversity = depositPaidUniversity;
            VisaStatus = visaStatus;
            RefundStatus = refundStatus;
            FutureIntake = futureIntake;
        }
    }
}
