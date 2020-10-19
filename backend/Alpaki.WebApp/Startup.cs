using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Alpaki.WebApp.Data;
using Alpaki.Moto.Database;
using Alpaki.CrossCutting.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<MotoDatabaseContext>(
            opt =>
                opt
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(connectionString, x => x.MigrationsHistoryTable("MotoMigrationsHistory", "System")),
            ServiceLifetime.Transient
);

            services.AddFactory<IMotoDatabaseContext, MotoDatabaseContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
