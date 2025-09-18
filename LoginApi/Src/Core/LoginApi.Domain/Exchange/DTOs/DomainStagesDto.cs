using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class DomainStagesDto
    {
        public DomainStagesDto()
        {
        }

        public DomainStagesDto(DomainStages domainStages)
        {
            Id = domainStages.Id;
            DomainId = domainStages.DomainId;
            StagesId = domainStages.StagesId;
            TemplateStatus = domainStages?.TemplateStatus;
            TemplateId = domainStages?.TemplateId;
            ParentId = domainStages?.ParentId;
            Status = domainStages.Status;
        }
        public string Id { get; set; }
        public string ? DomainId { get; set; }
        public string ? StagesId { get; set; }
        public string DomainStagesName { get; set; }
        public string ? DomainName { get; set; }
        public bool ? TemplateStatus { get; set; }
        public string ? TemplateId { get; set; }
        public string ? ParentId { get; set; }
        public string ? ParentName { get; set; }
        public int ? Status { get; set; }
    }
}
