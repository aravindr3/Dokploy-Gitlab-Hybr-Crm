using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Exchange.Entities;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class LeadDocumentDto
    {
        public LeadDocumentDto()
        {
        }

        public LeadDocumentDto(LeadDocument leadDocument)
        {
            Id = leadDocument.Id;
            LeadId = leadDocument.LeadId;
            Remark = leadDocument.Remark;
            FileName = leadDocument.FileName;
            FileType = leadDocument.FileType;
            FileSize = leadDocument.FileSize;
            Created = leadDocument.Created;
            Status = leadDocument.Status;
        }

        public string? Id { get; set; }
        public string LeadId { get; set; }
        public string FileName { get; set; }
        public string? Remark { get; set; }
        public string? FileType { get; set; }
        public string? FileSize { get; set; }
     
        public DateTime? Created { get; set; }
        public int Status { get; set; }
    }
}
