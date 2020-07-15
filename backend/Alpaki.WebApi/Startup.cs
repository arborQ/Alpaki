using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Logic;
using Alpaki.WebApi.Filters;
using Alpaki.WebApi.GraphQL;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using System.Text;
using MediatR;
using System.Reflection;

namespace Alpaki.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        private static byte[] FromBase64Url(string base64Url)
        {
            string base64 = string.Empty;
            if (!string.IsNullOrEmpty(base64Url))
            {
                string padded = base64Url.Length % 4 == 0
                    ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
                base64 = padded.Replace("_", "/")
                                        .Replace("-", "+");
            }
            return Convert.FromBase64String(base64);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("DefaultConnectionString");
            services.AddDbContext<IDatabaseContext, DatabaseContext>(
                opt =>
                    opt
                        .UseLoggerFactory(loggerFactory)
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(connectionString),
                ServiceLifetime.Transient
            );

            string privateSecretKey = Configuration.GetValue<string>("SeacretKey");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey))
                };
            });

            RegisterGraphQL(services);

            services.RegisterLogicServices();

            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(InitializeLogic).GetTypeInfo().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
            });
            
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(ApiKeyFilter));
                }
            );
            services.AddProblemDetails(
                opt =>
                {
                    opt.Map<ValidationException>(x => x.ToValidationProblemDetails());
                    opt.Map<LogicException>(x=>new StatusCodeProblemDetails(StatusCodes.Status400BadRequest)
                    {
                       Detail = x.Reason,
                       Extensions = { ["errorCode"] = x.Code }
                    });
                }
            );
        }

        private static void RegisterGraphQL(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(x =>
                new FuncDependencyResolver(x.GetRequiredService));
            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddDataLoader();

            RegisterServices(services);
            RegisterGraphQLSchemas(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }

        private static void RegisterGraphQLSchemas(IServiceCollection services)
        {
            services.AddScoped<DreamerSchema>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseContext databaseContext)
        {
            app.UseProblemDetails();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            databaseContext.Migrate();

            ConfigureSwagger(app);

            ConfigureGraphQL(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureGraphQL(IApplicationBuilder app)
        {
            app.UseGraphQL<DreamerSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/ql" });
        }

        private static void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alpaki API");
            });
        }
    }
}
