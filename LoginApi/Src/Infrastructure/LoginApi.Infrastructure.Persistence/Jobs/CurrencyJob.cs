using System;
using System.Threading.Tasks;
using HyBrForex.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Quartz;

namespace HyBrForex.Infrastructure.Persistence.Jobs;

public class CurrencyJob(ILogger<CurrencyJob> logger, ICurrencyBoardRepository currencyBoardRepository) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Execute Job");
        var message = $"CurrencyJob started at: {DateTime.Now}";
        logger.LogInformation(message);
        //var currencyRateUpdaterResponse = await currencyBoardRepository.GetCurrencyRateUpdater();
        //if (currencyRateUpdaterResponse.Data != null && currencyRateUpdaterResponse.Data.rates.Count > 0)
        //{
        //    foreach (var rate in currencyRateUpdaterResponse.Data.rates)
        //    {
        //        var ISD = rate.Key;
        //        message = $"ISD: {ISD}-------- Rate:{rate.Value}";
        //        logger.LogInformation(message);
        //    }

        //    message = $"CurrencyJob completed at: {DateTime.Now}";
        //    logger.LogInformation(message);
        //}

        await Task.CompletedTask;
    }
}