using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using MediatR;

namespace HyBrForex.Application.Features.ForexSlabConfig
{
    public class CreateForexSlabCommand : IRequest<BaseResult<List<string>>>
    {
        public List<CreateForexSlabDto> PurchaseDetails { get; set; }

        public class CreateForexSlabDto
        {
            public decimal FromAmount { get; set; }
            public decimal ToAmount { get; set; }

            public decimal GstBaseValue { get; set; }       // Fixed base GST value or calculated portion
            public decimal GstPercentage { get; set; }      // For percentage-based calculations
            public decimal GstRate { get; set; }            // GST tax rate, e.g., 18%
            public decimal AdditionalGstCharge { get; set; } // Any additional GST portion (if applicable)

            public bool IsFixed { get; set; }
        }
    }
   
}
