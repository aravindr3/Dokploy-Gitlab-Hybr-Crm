using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;
using HyBrForex.Domain.CurrencyBoard.DTO.Response;
using HyBrForex.Domain.CurrencyBoard.Entities;

namespace HyBrForex.Application.Interfaces.Repositories;

public interface ICBBannerRepository : IGenericRepository<BannerDtl>
{
    Task<BaseResult> AddBanner(BannerAddRequest request);

    Task<BaseResult> UpdateBanner(BannerUpdateRequest request);

    Task<BaseResult<BannerResponse>> GetBanneres();

    Task<BaseResult<BannerProperties>> GetBannerById(string BannerId);
}