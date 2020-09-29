using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.Logic
{
    public static class InitializeMotoLogic
    {
        public static IServiceCollection RegisterMotoLogicServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(InitializeMotoLogic));
            services.AddMediatR(typeof(InitializeMotoLogic).GetTypeInfo().Assembly);

            return services;
        }
    }
}
