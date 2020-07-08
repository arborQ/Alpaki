using System;
using System.Collections.Generic;
using System.Text;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services)
        {
            services.AddScoped<CreateDreamerRequestValidator>();

            return services;
        }
    }
}
