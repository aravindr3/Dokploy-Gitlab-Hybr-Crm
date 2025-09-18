using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.CurrencyBoard.Entities
{
    public class CurrencyBoardDtl : AuditableBaseEntity
    {
        public CurrencyBoardDtl(int index, string currencyId, decimal inrRate, decimal buyValue, decimal sellValue)
        {
            Index = index;
            CurrencyId = currencyId;
            InrRate = inrRate;
            BuyValue = buyValue;
            SellValue = sellValue;
        }

        public CurrencyBoardDtl()
        {
        }

        public int Index { get; set; }
        public string CurrencyId { get; set; }
        public decimal InrRate { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
    }
}