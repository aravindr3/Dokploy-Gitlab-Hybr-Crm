using System;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Infrastructure.Persistence.Contexts;
using HyBrForex.Infrastructure.Persistence.Jobs;
using HyBrForex.Infrastructure.Persistence.Repositories;
using HyBrForex.Infrastructure.Persistence.Services;
using HyBrForex.Infrastructure.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace HyBrForex.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services,
        IConfiguration configuration, bool useInMemoryDatabase)
    {
        if (useInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(nameof(ApplicationDbContext)));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Scoped);
            services.AddDbContext<ApplicationDbContext>(//options =>options.UseLazyLoadingProxies()
            );
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        var exchangeRateAPISettings =
            configuration.GetSection(nameof(ExchangeRateAPISettings)).Get<ExchangeRateAPISettings>();
        services.AddSingleton(exchangeRateAPISettings);

        services.AddHttpClient<ApiClientService>(client =>
        {
            client.BaseAddress = new Uri($"{exchangeRateAPISettings.BaseUrl}"); // Base URL of the API
           // client.BaseAddress = new Uri("https://openexchangerates.org/api/latest.json?app_id=9f920a7455ac4b54b7770d75d916a835"); // Base URL of the API
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromMinutes(6);
        });


        services.AddHttpClient("BonvoiceClient", client =>
        {
            client.BaseAddress = new Uri("https://backend.pbx.bonvoice.com/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(60);
        });


        services.AddControllers().AddFluentValidation(fv =>fv.RegisterValidatorsFromAssemblyContaining<CurrencyBoardRequestValidator>());

        // Configure Quartz
        services.AddQuartz(q =>
        {
            //q.UsePersistentStore(x =>
            //{
            //    x.UsePostgres(configuration.GetConnectionString("DefaultConnection"));
            //    x.UseJsonSerializer();
            //    x.UseClustering();
            //});

            // Register a job
            var jobKey = new JobKey("CurrencyJob");
            q.AddJob<CurrencyJob>(opts => opts.WithIdentity(jobKey));

            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("CurrencyTrigger")
                .WithCronSchedule("0 0 * * * ?")); // Runs every hour, at the start of the hour

            //q.AddTrigger(opts => opts
            //    .ForJob(jobKey)
            //    .WithIdentity("CurrencyTrigger")
            //    //.WithCronSchedule("0 0 12 * * ?")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x.WithIntervalInHours(8).RepeatForever())

            //    );
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.RegisterRepositories();

        return services;
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        var interfaceType = typeof(IGenericRepository<>);
        var interfaces = Assembly.GetAssembly(interfaceType)!.GetTypes()
            .Where(p => p.GetInterface(interfaceType.Name) != null);

        var implementations = Assembly.GetAssembly(typeof(GenericRepository<>))!.GetTypes();

        foreach (var item in interfaces)
        {
            var implementation = implementations.FirstOrDefault(p => p.GetInterface(item.Name) != null);

            if (implementation is not null)
                services.AddTransient(item, implementation);
        }
    }
}