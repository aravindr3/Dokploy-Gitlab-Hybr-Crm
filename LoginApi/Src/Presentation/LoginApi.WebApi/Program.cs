using System;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using HyBrForex.Application;
using HyBrForex.Application.Interfaces;
using HyBrForex.Application.Interfaces.Repositories;
using HyBrForex.Application.Interfaces.UserInterfaces;
using HyBrForex.Infrastructure.Identity;
using HyBrForex.Infrastructure.Identity.Services;
using HyBrForex.Infrastructure.Persistence;
using HyBrForex.Infrastructure.Persistence.Repositories;
using HyBrCRM.Infrastructure.Resources;
using HyBrForex.WebApi.Infrastructure.Extensions;
using HyBrForex.WebApi.Infrastructure.Filters;
using HyBrForex.WebApi.Infrastructure.Middlewares;
using HyBrForex.WebApi.Infrastructure.Services;
using LoginApi.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using HyBrCRM.Domain.Exchange.Entities;
using Microsoft.EntityFrameworkCore;
using HyBrCRM.Domain.Bonvoice.DTO.Request;
using HyBrCRM.Infrastructure.Persistence.Settings;
using HyBrCRM.Application.Interfaces.Repositories;
using Microsoft.Extensions.Options;

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




        // Configure Kestrel
        // Configure Kestrel to use HTTP protocol

        //builder.WebHost.ConfigureKestrel((context, options) =>
        //{
        //    options.Configure(context.Configuration.GetSection("Kestrel")); // Read Kestrel configuration from appsettings.json
        //    options.ListenUnixSocket("/tmp/kestrel-server.sock");

        //});

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

        // Get Time Zone from appsettings.json
        var timeZoneId = "Asia/Kolkata"; // Fallback to UTC
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        // Register Time Zone Service
        builder.Services.AddSingleton(timeZone);

        var app = builder.Build();

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
}