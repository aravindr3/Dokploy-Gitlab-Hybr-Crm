//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using HyBrForex.Application.Interfaces;
//using HyBrForex.Application.Interfaces.Repositories;
//using HyBrForex.Application.Wrappers;
//using HyBrForex.Domain.CurrencyBoard.DTO.Properties;
//using HyBrForex.Domain.CurrencyBoard.DTO.Request;
//using HyBrForex.Domain.CurrencyBoard.DTO.Response;
//using HyBrForex.Domain.CurrencyBoard.Entities;
//using HyBrForex.Infrastructure.Persistence.Contexts;
//using HyBrForex.Infrastructure.Persistence.Services;
//using Microsoft.EntityFrameworkCore;

//namespace HyBrForex.Infrastructure.Persistence.Repositories;

//public class CurrencyBoardRepository(
//    ApplicationDbContext applicationDbContext,
//    IUnitOfWork unitOfWork,
//    ApiClientService apiClientService)
//    : GenericRepository<CurrencyBoardDtl>(applicationDbContext), ICurrencyBoardRepository
//{
//    public async Task<BaseResult<string>> AddCurrency(CurrencyBoardRequest request)
//    {
//        var currencyBD = await applicationDbContext.CurrencyBoardDtl.Where(a => a.CurrencyId == request.CurrencyId)
//            .FirstOrDefaultAsync();

//        if (currencyBD == null)
//        {
//            var allNot = await GetAllAsync();
//            var index = allNot.Count == 0 ? 1 : allNot.Max(a => a.Index) + 1;
//#pragma warning disable S1854 // Unused assignments should be removed
//            _ = new CurrencyBoardDtl(index, request.CurrencyId, request.InrRate, request.BuyValue, request.SellValue);
//#pragma warning restore S1854 // Unused assignments should be removed
//            new CurrencyBoardDtl().Id = Ulid.NewUlid().ToString();
//            await AddAsync(new CurrencyBoardDtl());
//            await unitOfWork.SaveChangesAsync();
//            return BaseResult<string>.Ok(new CurrencyBoardDtl().Id);
//        }

//        var error = new Error(ErrorCode.BadRequest, "Currency already exists in Board",
//            "CurrencyId:" + request.CurrencyId);
//        return BaseResult<string>.Failure(error);
//    }

//    public async Task<BaseResult<string>> UpdateCurrency(CurrencyBoardUpdate request)
//    {
//        var currencyBoardDtl = new CurrencyBoardDtl();
//        if (request.Type == 1)
//        {
//            currencyBoardDtl = await GetByIdAsync(request.CurrencyBoardId);
//            if (currencyBoardDtl != null)
//            {
//                currencyBoardDtl.SellValue = Convert.ToDecimal(request.SellValue);
//                currencyBoardDtl.BuyValue = Convert.ToDecimal(request.BuyValue);
//            }

//            Update(currencyBoardDtl);
//            await unitOfWork.SaveChangesAsync();
//        }
//        else
//        {
//            currencyBoardDtl = await GetByIdAsync(request.CurrencyBoardId);
//            if (currencyBoardDtl != null) currencyBoardDtl.Status = request.Status;
//            Update(currencyBoardDtl);
//            await unitOfWork.SaveChangesAsync();
//        }

//        return BaseResult<string>.Ok(currencyBoardDtl.Id);
//    }

//    public async Task<BaseResult<CurrencyBoardProperties>> GetCurrencyBoardById(string CurrencyBoardId)
//    {
//        var currencyBoardDtl = await GetByIdAsync(CurrencyBoardId);
//        var currencyBoard = new CurrencyBoardProperties();
//        if (currencyBoardDtl != null)
//            currencyBoard = new CurrencyBoardProperties
//            {
//                Index = currencyBoard.Index,
//                CurrencyBoardId = currencyBoardDtl.Id,
//                CurrencyId = currencyBoardDtl.CurrencyId,
//                buyMargin = currencyBoardDtl.BuyValue,
//                BuyValue = currencyBoardDtl.InrRate - currencyBoardDtl.BuyValue,
//                SellValue = currencyBoardDtl.SellValue,
//                sellMargin = currencyBoardDtl.InrRate + currencyBoardDtl.SellValue,
//                InrRate = currencyBoardDtl.InrRate,
//                Status = currencyBoardDtl.Status
//            };

//        return BaseResult<CurrencyBoardProperties>.Ok(currencyBoard);
//    }

//    public async Task<BaseResult<CurrencyBoardResponse>> GetCurrencyBoards()
//    {
//        var response = new CurrencyBoardResponse();
//        var currencies = new List<CurrencyBoardProperties>();
//        var allNot = await GetAllAsync();
//        if (allNot.Count > 0)
//        {
//            currencies =
//            [
//                .. (from cb in applicationDbContext.CurrencyBoardDtl
//                    join c in applicationDbContext.CurrencyMaster on cb.CurrencyId equals c.Id
//                    join cc in applicationDbContext.CurrencyConversionDtl on c.Id equals cc.ToCurrencyId
//                    where cc.Status == 1
//                    select new CurrencyBoardProperties
//                    {
//                        CurrencyBoardId = cb.Id, CurrencyId = cb.CurrencyId, ISD = c.ISD, Index = cb.Index,
//                        InrRate = cc.ToCurrencyRate, buyMargin = cb.BuyValue,
//                        BuyValue = cc.ToCurrencyRate - cb.BuyValue, sellMargin = cb.SellValue,
//                        SellValue = cc.ToCurrencyRate + cb.SellValue, Status = cb.Status, CurrencyDate = cc.Created,
//                        time_last_update_utc = cc.time_last_update_utc
//                    }).OrderBy(a => a.Index)
//            ];
//            response.currencies = currencies;
//            if (response.currencies.Count > 0)
//                response.CurrencyDate = Convert.ToDateTime(currencies.Min(a => a.time_last_update_utc))
//                    .ToString("dd-MM-yyyy");
//        }

//        return BaseResult<CurrencyBoardResponse>.Ok(response);
//    }

//    public async Task<BaseResult<CurrencyRateUpdaterResponse>> GetCurrencyRateUpdater()
//    {
//        var currencyRateUpdaterResponse = await apiClientService.GetAsync<CurrencyRateUpdaterResponse>("");

//        var status = currencyRateUpdaterResponse.rates.Count > 0 ? "success" : "Fail";

//        if (status == "success")
//        {
//            //string dateString = currencyRateUpdaterResponse.time_last_update_utc;
//            //string format = "ddd, dd MMM yyyy HH:mm:ss zzz";
//            //DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

//            var unixTimestamp = Convert.ToInt64(currencyRateUpdaterResponse.timestamp);
//            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
//            var dateTimes = epoch.AddSeconds(unixTimestamp);

//            var inrrate = (decimal)currencyRateUpdaterResponse.rates["INR"];
//            var conversion_rates = currencyRateUpdaterResponse.rates;

//            foreach (var rate in conversion_rates)
//            {
//                var ISD = rate.Key;
//                var Currate = (decimal)rate.Value;

//                var CurrencyCurrency = applicationDbContext.CurrencyMaster.Where(a => a.ISD == ISD).FirstOrDefault();
//                if (CurrencyCurrency != null)
//                {
//                    var CIsd = CurrencyCurrency.ISD;
//                    if (ISD == CIsd)
//                    {
//                        // var timeZoneId = "Asia/Kolkata"; // Fallback to UTC
//                        var currentTime = dateTimes;
//                        var time_last_update_utc = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
//                            currentTime.Hour, currentTime.Minute, 0, DateTimeKind.Utc);

//                        var rateInInr = ConvertCurrency(1, Currate, inrrate);
//                        var currencyConversionDtl = new CurrencyConversionDtl
//                        {
//                            BaseCurrencyId = applicationDbContext.CurrencyMaster.Where(a => a.ISD == "USD")
//                                .FirstOrDefault().Id,
//                            ToCurrencyId = applicationDbContext.CurrencyMaster.Where(a => a.ISD == CIsd)
//                                .FirstOrDefault().Id,
//                            BaseCurrencyRate = Currate,
//                            ToCurrencyRate = rateInInr,
//                            Status = 1,
//                            time_last_update_utc = time_last_update_utc,
//                            Id = Ulid.NewUlid().ToString()
//                        };
//                        var _currencyConversionDtl = await applicationDbContext.CurrencyConversionDtl
//                            .Where(a => a.Status == 1 && a.ToCurrencyId == currencyConversionDtl.ToCurrencyId)
//                            .FirstOrDefaultAsync();
//                        if (_currencyConversionDtl != null)
//                        {
//                            _currencyConversionDtl.Status = 0;
//                            applicationDbContext.Update(_currencyConversionDtl);
//                            await unitOfWork.SaveChangesAsync();
//                        }

//                        await applicationDbContext.AddAsync(currencyConversionDtl);
//                        await unitOfWork.SaveChangesAsync();
//                    }
//                }
//            }
//        }

//        return BaseResult<CurrencyRateUpdaterResponse>.Ok(currencyRateUpdaterResponse);
//    }

//    public async Task<BaseResult<CurrencyResponse>> GetCurrency()
//    {
//        var response = new CurrencyResponse();
//        var currencies = new List<CurrencyProperties>();

//        currencies =
//        [
//            .. from cb in applicationDbContext.CurrencyMaster
//            join c in applicationDbContext.CurrencyConversionDtl on cb.Id equals c.ToCurrencyId
//            where cb.Status == 1 && c.Status == 1
//            select new CurrencyProperties
//            {
//                Currency = cb.Id, CurrencyName = cb.Currency, ISD = cb.ISD, CurrencyRate = c.ToCurrencyRate,
//                Status = cb.Status
//            }
//        ];
//        response.currencies = currencies;
//        return await Task.FromResult<BaseResult<CurrencyResponse>>(response);
//    }

//    public static decimal ConvertCurrency(decimal amount, decimal sourceToBaseRate, decimal baseToTargetRate)
//    {
//        if (sourceToBaseRate <= 0 || baseToTargetRate <= 0)
//            throw new ArgumentException("Exchange rates must be greater than zero.");

//        // Convert the amount to base currency, then to the target currency
//        var amountInBase = amount / sourceToBaseRate;
//        var convertedAmount = amountInBase * baseToTargetRate;

//        return convertedAmount;
//    }



//    public async Task<BaseResult<CurrencyResponse>> GetCurrencyByCurrencyId(string CurrencyId)
//    {
//        var response = new CurrencyResponse();

//        var currencies = await (
//            from cb in applicationDbContext.CurrencyMaster
//            join c in applicationDbContext.CurrencyConversionDtl on cb.Id equals c.ToCurrencyId
//            where cb.Status == 1 && c.Status == 1 && cb.Id == CurrencyId
//            select new CurrencyProperties
//            {
//                Currency = cb.Id,
//                CurrencyName = cb.Currency,
//                ISD = cb.ISD,
//                CurrencyRate = c.ToCurrencyRate,
//                Status = cb.Status
//            }
//        ).ToListAsync();

//        if (currencies is null || !currencies.Any())
//            return new Error(ErrorCode.NotFound, "Currency not found");

//        response.currencies = currencies;
//        return BaseResult<CurrencyResponse>.Ok(response);
//    }


//}