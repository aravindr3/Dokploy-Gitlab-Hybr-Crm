using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CurrencyMasterDto
    {
        public CurrencyMasterDto()
        {
        }

        public CurrencyMasterDto(CurrencyMaster currencyMaster)
        {
            CountryId = currencyMaster.CountryId;
            ISD = currencyMaster.ISD;
            Currency = currencyMaster.Currency;
            FLM8Cd = currencyMaster.FLM8Cd;
            Id = currencyMaster.Id;
            Status = currencyMaster.Status;
            CountryName = currencyMaster.CountryName;
        }

        public CurrencyMasterDto(string countryId, string iSD, string currency, string? fLM8Cd)
        {
            CountryId = countryId;
            ISD = iSD;
            Currency = currency;
            FLM8Cd = fLM8Cd;
        }

        public string? CountryName { get; set; }
        public string? Id { get; set; }
        public string? CountryId { get; set; }
        public string? ISD { get; set; }
        public string? Currency { get; set; }
        public string? FLM8Cd { get; set; }
        public int Status { get; set; }
    }
}