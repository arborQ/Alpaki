﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Alpaki.CrossCutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFactory<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<TService, TImplementation>();
            services.AddSingleton<Func<TService>>(x => () => x.GetRequiredService<TService>());
        }
    }
}
