using System.Collections.Generic;
using HyBrForex.Domain.Common;

namespace HyBrForex.Domain.Exchange.Entities
{
    public class CurrencyMaster : AuditableBaseEntity
    {
        public CurrencyMaster()
        {
        }

        public CurrencyMaster(string countryId, string iSD, string currency, string? fLM8Cd)
        {
            CountryId = countryId;
            ISD = iSD;
            Currency = currency;
            FLM8Cd = fLM8Cd;
        }

        public string CountryId { get; set; }
        public string ISD { get; set; }
        public string Currency { get; set; }
        public string? FLM8Cd { get; set; }
        public byte[]? CurrencyFlag { get; set; }
        public string? CountryName { get; set; }

        public virtual CountryMaster? CountryMaster { get; set; }


        public void Update(string iSD, string currency, string fLM8Cd)
        {
            ISD = iSD;
            Currency = currency;
            FLM8Cd = fLM8Cd;
        }
    }
}