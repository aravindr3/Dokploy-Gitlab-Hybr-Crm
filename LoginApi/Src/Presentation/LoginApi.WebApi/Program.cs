using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using HyBrCRM.Application.Interfaces.Repositories;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Domain.Exchange.Entities;
using HyBrCRM.Infrastructure.Persistence.Settings;
using HyBrCRM.Infrastructure.Resources;
using HyBrForex.Application;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Infrastructure.Persistence;
using HyBrForex.Infrastructure.Persistence.Repositories;
using HyBrForex.WebApi.Infrastructure.Extensions;
using HyBrForex.WebApi.Infrastructure.Filters;
using HyBrForex.WebApi.Infrastructure.Middlewares;
using HyBrForex.WebApi.Infrastructure.Services;
using LoginApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace HyBrForex.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");
        builder.Services.AddRazorPages();

        builder.Services.AddApplicationLayer();
        builder.Services.AddPersistenceInfrastructure(builder.Configuration, useInMemoryDatabase);
        builder.Services.AddIdentityInfrastructure(builder.Configuration, useInMemoryDatabase);
        builder.Services.AddResourcesInfrastructure();
        builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddSwaggerWithVersioning();
        builder.Services.AddAnyCors();
        builder.Services.AddCustomLocalization(builder.Configuration);
        builder.Services.AddHealthChecks();
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        builder.Services.AddScoped<AuthenticationFilter>();

        builder.Services.Configure<BonvoiceSettings>(
    builder.Configuration.GetSection("BonvoiceSettings"));

        builder.Services.AddSingleton<IBonvoiceSettings>(sp =>
            sp.GetRequiredService<IOptions<BonvoiceSettings>>().Value);

        builder.Services.Configure<AutoCallSettings>(
   builder.Configuration.GetSection("AuthCallSettings"));

        builder.Services.AddSingleton<IAutoCallSettings>(sp =>
            sp.GetRequiredService<IOptions<AutoCallSettings>>().Value);

        builder.Services.AddDataProtection()
     .PersistKeysToFileSystem(new DirectoryInfo(@"/keys"))
     .SetApplicationName("HybrCrmWebApi");
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);


        // Configure Kestrel
        // Configure Kestrel to use HTTP protocol

        //builder.WebHost.ConfigureKestrel((context, options) =>
        //{
        //    options.Configure(context.Configuration.GetSection("Kestrel")); // Read Kestrel configuration from appsettings.json
        //    options.ListenUnixSocket("/tmp/kestrel-server.sock");

        //});
        Console.WriteLine("Kestrel configuration completed.");
        // Configure Kestrel to use HTTP protocol
        builder.WebHost.ConfigureKestrel(options =>
        {
            // Example: Listen on port 5000 for HTTP
            //options.ListenAnyIP(Convert.ToInt32(builder.Configuration["KestrelSettings:Httpport"]), listenOptions =>
            //{
            //    listenOptions.Protocols = HttpProtocols.Http1; // Explicitly use HTTP/1.1
            //});
            options.Limits.MaxRequestBodySize = 52428800; // 50 MB
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);

            // Example: Optionally enable HTTP/2 on a different endpoint

            //options.ListenAnyIP(5001, listenOptions =>
            //{
            //    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1; // Use HTTP/2
            //    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2; // Use HTTP/2
            //    listenOptions.UseHttps(httpsOptions =>
            //    {
            //        // Enable only TLS 1.2 and TLS 1.3
            //        httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            //                                  | System.Security.Authentication.SslProtocols.Tls13;
            //    });
            //});

            //options.ListenUnixSocket("/tmp/kestrel-server.sock");
        });
        Console.WriteLine("Kestrel configuration completed.");
        // Get Time Zone from appsettings.json
        var timeZoneId = "Asia/Kolkata"; // Fallback to UTC
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        // Register Time Zone Service
        builder.Services.AddSingleton(timeZone);
      
        var app = builder.Build();
        var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast");

        //app.UseExceptionHandler(a => a.Run(async context =>
        //{
        //    Console.WriteLine("exception");
        //    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        //    var exception = exceptionHandlerPathFeature.Error;
        //    Console.WriteLine(exception.Message);
        //    var result = JsonConvert.SerializeObject(new { error = exception.Message });
        //    context.Response.ContentType = "application/json";
        //    await context.Response.WriteAsync(result);
        //}));
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            //if (!useInMemoryDatabase)
            //{
            //    await services.GetRequiredService<IdentityContext>().Database.MigrateAsync();
            //    await services.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
            //    await services.GetRequiredService<FileManagerDbContext>().Database.MigrateAsync();
            //}

            //   Seed Data
            //await DefaultRoles.SeedAsync(services.GetRequiredService<RoleManager<ApplicationRole>>());
            //await DefaultBasicUser.SeedAsync(services.GetRequiredService<UserManager<ApplicationUser>>(),
            // services.GetRequiredService<RoleManager<ApplicationRole>>());
            //await DefaultData.SeedAsync(services.GetRequiredService<ApplicationDbContext>());
        }
        var provider = new FileExtensionContentTypeProvider();
        // Override or add mappings if necessary
        provider.Mappings[".js"] = "application/javascript";

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = provider
        });
        app.UseCustomLocalization();
        app.UseAnyCors();
        app.UseRouting();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwaggerWithVersioning();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseHealthChecks("/health");
        app.MapControllers();
        app.UseSerilogRequestLogging();
        app.MapRazorPages();
        app.MapGet("/", () => Results.Redirect("/Index"));
        await app.RunAsync();
    }
    internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}