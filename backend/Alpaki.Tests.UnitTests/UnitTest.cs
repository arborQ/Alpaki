using System;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Logic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Alpaki.Tests.UnitTests
{
    [Collection("UnitTests")]
    public class UnitTest
    {
        private readonly IMediator _mediator;
        protected readonly IServiceProvider RootProvider;
        public UnitTest(Action<IServiceCollection> replaceServices = null)
        {
            IServiceCollection services = new ServiceCollection();
            services.RegisterLogicServices();

            services.AddDbContext<IDatabaseContext, DatabaseContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddSingleton<IJwtGenerator>(Substitute.For<IJwtGenerator>());
            replaceServices?.Invoke(services);

            RootProvider = services.BuildServiceProvider();
            _mediator = RootProvider.GetService<IMediator>();
        }

        protected async Task<TResponse> Send<TResponse>(IRequest<TResponse> request) 
            => await _mediator.Send(request);
    }
}