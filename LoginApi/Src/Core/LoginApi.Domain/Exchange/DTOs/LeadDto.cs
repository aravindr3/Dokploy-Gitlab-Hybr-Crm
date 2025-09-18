using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadDto
    {
        public LeadDto()
        {
        }

        public LeadDto(Lead lead)
        {
            LeadSourceId = lead?.LeadSourceId;
            LeadSourceName = lead?.LeadSource?.CommonName;
            LeadCategoryId = lead?.CategoryId;
            LeadStatusId = lead?.LeadStatusId;
            VerticalId = lead?.VericalId;
            CategoryId = lead?.CategoryId;
            Notes = lead?.Notes;
            PreferedCountry = lead?.PreferedCountry;
            Created = lead?.Created;

        }
        public string ? Id { get; set; }
        public string? LeadSourceId { get; set; }
        public string ? LeadSourceName { get; set; }
        public string ? LeadCategoryId { get; set; }
        public string ? LeadCategoryName { get; set; }
        public string? LeadStatusId { get; set; }
        public string ? LeadStatusName { get; set; }
        public string? VerticalId { get; set; }
        public string? VerticalName { get; set; }
        public string? Notes { get; set; }
        public string ? CategoryId { get; set; }
        public string ? CategoryName { get; set; }
        public string ? PreferedCountry {  get; set; }
        public string ? PreferedCountryName { get; set; }
        public DateTime ? Created { get; set; }
        public DateTime ? LastTask {  get; set; }
        public string ? Stage {  get; set; }
        public int ? Status { get; set; }
        public LeadContactDto? Contact { get; set; }
        public List<LeadPropertyValueDto> ? LeadProperties { get; set; }
        public List<LeadDocumentDto>? LeadDocument { get; set; }


    }
}
