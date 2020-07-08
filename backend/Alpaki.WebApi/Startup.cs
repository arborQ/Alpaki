using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Alpaki.Database;
using MediatR;
using System.Reflection;
using Alpaki.Logic;
using GraphQL;
using GraphQL.Server;
using Alpaki.WebApi.GraphQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Alpaki.WebApi.Filters;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Expressions;

namespace Alpaki.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("DefaultConnectionString");
            services.AddDbContext<DatabaseContext>(opt =>
               opt.UseSqlServer(connectionString), ServiceLifetime.Transient);

            RegisterGraphQL(services);

            services.AddControllers();
            services.AddMediatR(typeof(InitializeLogic).GetTypeInfo().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ApiKeyFilter));
            });
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
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<VolontierDreamerExpressions>();
            services.AddTransient<VolontierUserExpressions>();
            services.RegisterLogicServices();
        }

        private static void RegisterGraphQLSchemas(IServiceCollection services)
        {
            services.AddScoped<DreamerSchema>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext databaseContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                databaseContext.Database.EnsureCreated();
            }

            ConfigureSwagger(app);

            ConfigureGraphQL(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureGraphQL(IApplicationBuilder app)
        {
            app.UseGraphQL<DreamerSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
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
