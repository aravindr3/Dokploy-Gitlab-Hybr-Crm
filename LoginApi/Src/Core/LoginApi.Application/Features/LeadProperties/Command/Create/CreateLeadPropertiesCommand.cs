using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.LeadProperties.Command.Create
{
    public class CreateLeadPropertiesCommand : IRequest<BaseResult<string>>
    {
        //public string? OwnerId { get; set; }
        //public string? LeadId { get; set; }
        //public string? CountryInterestedIn { get; set; }
        //public bool? DocumentUploadStatus { get; set; }
        //public string? UniversityPreferred { get; set; }
        //public int? OfferLetterStatus { get; set; }
        //public bool? DepositPaidUniversity { get; set; }
        //public bool? VisaStatus { get; set; }
        //public int? RefundStatus { get; set; }
        //public DateTime? FutureIntake { get; set; }

        public string? OwnerId { get; set; }
        public string? LeadId { get; set; }
        public string? Domain { get; set; }

        // Dynamic field values based on metadata (key = field name)
        public Dictionary<string, string?> Properties { get; set; } = new();
    }
}
