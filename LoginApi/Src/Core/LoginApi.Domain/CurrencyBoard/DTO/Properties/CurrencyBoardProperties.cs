using System;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Properties
{
    public class CurrencyProperties
    {
        public string? Currency { get; set; }

        public string? CurrencyName { get; set; }
        public string? ISD { get; set; }
        public decimal CurrencyRate { get; set; }
        public int Status { get; set; }
    }

    public class CurrencyBoardProperties
    {
        public int Index { get; set; }
        public int Status { get; set; }
        public string? CurrencyId { get; set; }
        public string? CurrencyBoardId { get; set; }
        public decimal InrRate { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
        public decimal buyMargin { get; set; }
        public decimal sellMargin { get; set; }

        //public string CurrencyFlag { get; set; }
        public string? ISD { get; set; }

        public DateTime? CurrencyDate { get; set; }
        public DateTime? time_last_update_utc { get; set; }
    }
}