using System;
using System.Collections.Generic;
using System.Text;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CurrencyDetailDto
    {
        public string CurrencyCode { get; set; }
        public decimal Denomination { get; set; }
        public int Quantity { get; set; }
        public decimal ForeignAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal InrAmount { get; set; }
    }
}
