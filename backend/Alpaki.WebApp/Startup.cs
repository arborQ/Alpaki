using System;
using System.Collections.Generic;
using System.Globalization;
using Alpaki.CrossCutting.Extensions;
using Alpaki.TimeSheet.Database;
using Alpaki.TimeSheet.Logic;
using Alpaki.WebApp.Data;
using Alpaki.WebApp.Store;
using Blazored.LocalStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Alpaki.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            var connectionString = Configuration.GetValue<string>("DefaultConnectionString");
            services.AddDbContext<TimeSheetDatabaseContext>(
            opt =>
                opt
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging()
                    .EnableServiceProviderCaching()
                    .UseSqlServer(connectionString, x => x.MigrationsHistoryTable("TimeSheetMigrationsHistory", "System")),
            ServiceLifetime.Transient
            );

            services.AddTransient<ITimeSheetDatabaseContext, TimeSheetDatabaseContext>();
            services.AddFactory<ITimeSheetDatabaseContext, TimeSheetDatabaseContext>();

            services.RegisterTimeSheetLogicServices();

            services.AddHttpClient();
            services.AddSingleton<StateContainer>();
            services.AddBlazoredLocalStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITimeSheetDatabaseContext timeSheetDatabaseContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            timeSheetDatabaseContext.Migrate();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
            var defaultDateCulture = "pl-PL";
            var ci = new CultureInfo(defaultDateCulture);
            ci.NumberFormat.NumberDecimalSeparator = ".";
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
        }
    }
}
