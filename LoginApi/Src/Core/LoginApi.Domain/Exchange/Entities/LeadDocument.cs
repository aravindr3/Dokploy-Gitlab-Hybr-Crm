using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class LeadDocument : AuditableBaseEntity
    {
        public LeadDocument()
        {
        }

        public LeadDocument(string leadId, string fileName, string? remark, string? fileType, string? fileSize , string ? taskId)
        {
            LeadId = leadId;
            FileName = fileName;
            Remark = remark;
            FileType = fileType;
            FileSize = fileSize;
            TaskId = taskId;
        }

        public string LeadId { get; set; }
        public string FileName { get; set; }
        public string? Remark { get; set; }
        public string? FileType { get; set; }
        public string? FileSize { get; set; }
        public string ? TaskId { get; set; }

        public void Update( string remark,  string filename, string fileType, string fileSizeStr , string ? taskId)
        {
            Remark  = remark ?? string.Empty;
            FileName = filename;
            FileType = fileType;
            FileSize = fileSizeStr;
            TaskId = taskId;
        }
    }
}
