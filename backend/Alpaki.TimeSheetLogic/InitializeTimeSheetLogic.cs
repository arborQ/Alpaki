using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.TimeSheet.Logic
{
    public static class InitializeTimeSheetLogic
    {
        public static IServiceCollection RegisterTimeSheetLogicServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(InitializeTimeSheetLogic));
            services.AddMediatR(typeof(InitializeTimeSheetLogic).GetTypeInfo().Assembly);

            return services;
        }
    }
}
