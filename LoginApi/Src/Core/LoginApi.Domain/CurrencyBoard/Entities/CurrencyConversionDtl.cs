using System;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.CurrencyBoard.Entities
{
    public class CurrencyConversionDtl : AuditableBaseEntity
    {
        public string? BaseCurrencyId { get; set; }
        public string? ToCurrencyId { get; set; }
        public decimal BaseCurrencyRate { get; set; }
        public decimal ToCurrencyRate { get; set; }
        public DateTime time_last_update_utc { get; set; }
    }
}