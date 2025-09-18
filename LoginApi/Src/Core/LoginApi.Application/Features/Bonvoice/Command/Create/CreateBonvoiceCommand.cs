using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrCRM.Application.Features.Bonvoice.Command.Create
{
    public class CreateBonvoiceCommand : IRequest<BaseResult<string>>
    {
        public string Direction { get; set; }
        public string SourceNumber { get; set; }
        public string DestinationNumber { get; set; }
        public string DisplayNumber { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double CallDuration { get; set; }

        public string Status { get; set; } 

        public string DataSource { get; set; }

        public int CallType { get; set; } 

        public string DTMF { get; set; } 

        public string AccountID { get; set; }

        public string ResourceURL { get; set; }

    }
}
