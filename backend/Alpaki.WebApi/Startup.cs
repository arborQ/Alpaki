using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Logic;
using Alpaki.Logic.Services;
using Alpaki.Moto.Database;
using Alpaki.WebApi.Behaviors;
using Alpaki.WebApi.Filters;
using Alpaki.WebApi.GraphQL.DreamQuery;
using Alpaki.WebApi.GraphQL.MotoQuery;
using Alpaki.WebApi.GraphQL.MotoQuery.Types;
using Alpaki.WebApi.Policies;
using Alpaki.WebApi.Swagger;
using FluentValidation;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Alpaki.WebApi
{
    [ExcludeFromCodeCoverage]
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
            services.AddDbContext<DatabaseContext>(
                opt =>
                    opt
                        .UseLoggerFactory(loggerFactory)
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(connectionString, x => x.MigrationsHistoryTable("DreamsMigrationsHistory", "System")),
                ServiceLifetime.Transient
            );

            services.AddDbContext<MotoDatabaseContext>(
                opt =>
                    opt
                        .UseLoggerFactory(loggerFactory)
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(connectionString, x => x.MigrationsHistoryTable("MotoMigrationsHistory", "System")),
                ServiceLifetime.Transient
            );

            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<IMotoDatabaseContext, MotoDatabaseContext>();

            var seacretKey = Configuration.GetValue<string>($"{nameof(JwtConfig)}:{nameof(JwtConfig.SeacretKey)}");

            services.AddAuthorization(options =>
            {
                var adminClaims = new[] { UserRoleEnum.Admin }.Select(c => ((int)c).ToString()).ToArray();
                var coordinatorClaims = new[] { UserRoleEnum.Coordinator, UserRoleEnum.Admin }.Select(c => ((int)c).ToString()).ToArray();
                var volunteerClaims = new[] { UserRoleEnum.Admin, UserRoleEnum.Coordinator, UserRoleEnum.Volunteer }.Select(c => ((int)c).ToString()).ToArray();

                options.AddPolicy(AdminAccessAttribute.PolicyName, policy => policy.RequireClaim(ClaimTypes.Role, adminClaims));
                options.AddPolicy(CoordinatorAccessAttribute.PolicyName, policy => policy.RequireClaim(ClaimTypes.Role, coordinatorClaims));
                options.AddPolicy(VolunteerAccessAttribute.PolicyName, policy => policy.RequireClaim(ClaimTypes.Role, volunteerClaims));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(seacretKey))
                };
            });

            RegisterGraphQL(services);

            services.RegisterLogicServices();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(ApiKeyFilter));
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                }
            );
            services.AddHealthChecks().AddDbContextCheck<DatabaseContext>();
            services.AddHealthChecks().AddDbContextCheck<MotoDatabaseContext>();
            services.AddLogging(loggingBuilder =>
            {
                var seqConfigruation = Configuration.GetSection("Seq");
                var apiKey = seqConfigruation.GetValue<string>("ApiKey");

                if (!string.IsNullOrEmpty(apiKey))
                {
                    loggingBuilder.AddSeq(seqConfigruation);
                    Console.WriteLine($"[Log]: Using Seq: [{apiKey}]");
                }
                else
                {
                    loggingBuilder.AddConsole();
                    Console.WriteLine($"[Log]: Using Console");
                }
            });

            services.AddProblemDetails(
                opt =>
                {
                    opt.Map<ValidationException>(x => x.ToValidationProblemDetails());
                    opt.Map<LogicException>(x => new StatusCodeProblemDetails(StatusCodes.Status400BadRequest)
                    {
                        Detail = x.Reason,
                        Extensions = { ["errorCode"] = x.Code }
                    });
                }
            );

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
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

            services.AddTransient<BrandsQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseContext databaseContext, IMotoDatabaseContext motoDatabaseContext)
        {
            app.UseProblemDetails();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            databaseContext.Migrate();
            motoDatabaseContext.Migrate();

            ConfigureSwagger(app);

            ConfigureGraphQL(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        private static void ConfigureGraphQL(IApplicationBuilder app)
        {
            app.UseGraphQL<DreamerSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { });
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
