using System;
using System.Collections.Generic;
using System.Text;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CurrencyItemDto
    {
        public string CurrencyName { get; set; }
        public string SACCode { get; set; }
        public string Type { get; set; } = "CN"; // CN = Currency Note
        public string Value { get; set; } = "1.0000";
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal RupeeEquivalent => Math.Round(Amount * Rate, 2);
    }
}
