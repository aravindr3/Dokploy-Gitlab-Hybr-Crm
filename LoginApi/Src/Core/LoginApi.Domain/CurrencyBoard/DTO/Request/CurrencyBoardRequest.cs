namespace HyBrForex.Domain.CurrencyBoard.DTO.Request
{
    public class CurrencyBoardRequest
    {
        public int Index { get; set; }
        public string? CurrencyId { get; set; }
        public decimal InrRate { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
    }

    public class CurrencyBoardUpdate
    {
        public string? CurrencyBoardId { get; set; }
        public string? CurrencyId { get; set; }
        public decimal? BuyValue { get; set; }
        public decimal? SellValue { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}