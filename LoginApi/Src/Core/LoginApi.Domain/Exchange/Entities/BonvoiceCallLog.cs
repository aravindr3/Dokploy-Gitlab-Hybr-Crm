using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class BonvoiceCallLog : AuditableBaseEntity
    {
        public BonvoiceCallLog()
        {
        }

        public BonvoiceCallLog(string? leadId, string token, string tokenType, string headerKey, string headerValue, string? callStatus , string ? tasKId , string ? eventId)
        {
            LeadId = leadId;
            Token = token;
            TokenType = tokenType;
            HeaderKey = headerKey;
            HeaderValue = headerValue;
            CallStatus = callStatus;
            TaskId = tasKId;
            EventId = eventId;
        }

        public string ? LeadId { get; set; }
        public string Token { get; set; }

        public string TokenType { get; set; }

        public string HeaderKey { get; set; }

        public string HeaderValue { get; set; }
        public string ? CallStatus { get; set; }
        public string ? EventId { get; set; }
        public string ? TaskId { get; set; }
        public string ? CallRequest {  get; set; }
        public string ? CallResponse { get; set; }
    }
}
