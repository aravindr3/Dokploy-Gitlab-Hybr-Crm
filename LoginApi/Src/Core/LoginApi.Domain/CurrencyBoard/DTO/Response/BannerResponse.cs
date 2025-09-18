using System.Collections.Generic;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;

namespace HyBrForex.Domain.CurrencyBoard.DTO.Response
{
    public class BannerResponse
    {
        public List<BannerProperties>? Banners { get; set; }
    }
}