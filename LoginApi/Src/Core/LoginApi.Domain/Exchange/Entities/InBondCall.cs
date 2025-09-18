using System;
using System.Collections.Generic;
using System.Text;
using HyBrForex.Domain.Common;

namespace HyBrCRM.Domain.Exchange.Entities
{
    public class InBondCall : AuditableBaseEntity
    {
        public InBondCall()
        {
        }

        public InBondCall(string direction, string sourceNumber, string destinationNumber,
            string displayNumber, DateTime startTime, DateTime endTime, double callDuration,
            string callStatus, string dataSource, int callType, string dTMF, string accountID, string resourceURL)
        {
            Direction = direction;
            SourceNumber = sourceNumber;
            DestinationNumber = destinationNumber;
            DisplayNumber = displayNumber;
            StartTime = startTime;
            EndTime = endTime;
            CallDuration = callDuration;
            CallStatus = callStatus;
            DataSource = dataSource;
            CallType = callType;
            DTMF = dTMF;
            AccountID = accountID;
            ResourceURL = resourceURL;
        }

        public string Direction { get; set; }
        public string SourceNumber { get; set; }
        public string DestinationNumber { get; set; }
        public string DisplayNumber { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double CallDuration { get; set; }

        public string CallStatus { get; set; }

        public string DataSource { get; set; }

        public int CallType { get; set; }

        public string DTMF { get; set; }

        public string AccountID { get; set; }

        public string ResourceURL { get; set; }
    }
}
