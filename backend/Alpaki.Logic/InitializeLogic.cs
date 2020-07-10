using System;
using System.Collections.Generic;
using System.Text;
using Alpaki.Logic.Features.Dreamer.CreateDreamer;
using Alpaki.Logic.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services)
        {
            services.AddScoped<CreateDreamerRequestValidator>();
            services.AddScoped<IValidator<UserAuthorizeRequest>, UserAuthorizeRequestValidation>();

            return services;
        }
    }
}
