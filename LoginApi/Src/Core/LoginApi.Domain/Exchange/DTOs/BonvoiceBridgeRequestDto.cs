using System;
using System.Collections.Generic;
using System.Text;
using HyBrCRM.Domain.Bonvoice.DTO.Request;

namespace HyBrCRM.Domain.Exchange.DTOs
{
    public class BonvoiceBridgeRequestDto
    {
        public AuthRequest AuthRequest { get; set; }
        public AutoCallRequestDto CallRequest { get; set; }
    }
}
