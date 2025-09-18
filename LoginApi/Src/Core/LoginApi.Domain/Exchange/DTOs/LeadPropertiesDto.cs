using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadPropertiesDto
    {
        public LeadPropertiesDto()
        {
        }

        public LeadPropertiesDto(LeadProperties leadProperties)
        {
            Id = leadProperties.Id;
            OwnerId = leadProperties.OwnerId;
            LeadId = leadProperties.LeadId;
            CountryInterestedIn = leadProperties.CountryInterestedIn;
            DocumentUploadStatus = leadProperties.DocumentUploadStatus;
            UniversityPreferred = leadProperties.UniversityPreferred;
            OfferLetterStatus = leadProperties.OfferLetterStatus;
            DepositPaidUniversity = leadProperties.DepositPaidUniversity;
            VisaStatus = leadProperties.VisaStatus;
            RefundStatus = leadProperties.RefundStatus;
            FutureIntake = leadProperties.FutureIntake;
            Status = leadProperties.Status;
            
        }
        public string ? Id { get; set; }
        public string? OwnerId { get; set; }
        public string ? OwnerName { get; set; }
        public string? LeadId { get; set; }
        public string? CountryInterestedIn { get; set; }
        public string ? CountryName { get; set; }
        public bool? DocumentUploadStatus { get; set; }
        public string? UniversityPreferred { get; set; }
        public int? OfferLetterStatus { get; set; }
        public bool? DepositPaidUniversity { get; set; }
        public bool? VisaStatus { get; set; }
        public int? RefundStatus { get; set; }
        public DateTime? FutureIntake { get; set; }
        public int ? Status { get; set; }

    }
}
