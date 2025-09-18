using HyBrForex.Domain.Exchange.Entities;

namespace HyBrForex.Domain.Exchange.DTOs
{
    public class CountryMasterDto
    {
        public CountryMasterDto()
        {
        }

        public CountryMasterDto(CountryMaster countryMaster)
        {
            Id = countryMaster.Id;
            CountryName = countryMaster.CountryName;
            SwiftCode = countryMaster.SwiftCode;
        }

        public CountryMasterDto(string id, string countryName, string swiftcode)
        {
            Id = id;
            CountryName = countryName;
            SwiftCode = swiftcode;
        }

        public string? Id { get; set; }
        public string? CountryName { get; set; }
        public int Status { get; set; }
        public string? SwiftCode { get; set; }
    }
}