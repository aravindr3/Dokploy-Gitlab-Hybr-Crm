using System.Threading.Tasks;
using HyBrForex.Application.Wrappers;
using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
using HyBrForex.Domain.CurrencyBoard.DTO.Request;
using HyBrForex.Domain.CurrencyBoard.DTO.Response;
using HyBrForex.Domain.CurrencyBoard.Entities;

namespace HyBrForex.Application.Interfaces.Repositories;

public interface ICurrencyBoardRepository : IGenericRepository<CurrencyBoardDtl>
{
    Task<BaseResult<string>> AddCurrency(CurrencyBoardRequest request);

    Task<BaseResult<string>> UpdateCurrency(CurrencyBoardUpdate request);

    Task<BaseResult<CurrencyBoardResponse>> GetCurrencyBoards();

    Task<BaseResult<CurrencyBoardProperties>> GetCurrencyBoardById(string CurrencyBoardId);

    Task<BaseResult<CurrencyRateUpdaterResponse>> GetCurrencyRateUpdater();

    Task<BaseResult<CurrencyResponse>> GetCurrency();
    Task<BaseResult<CurrencyResponse>> GetCurrencyByCurrencyId(string CurrencyId);


}