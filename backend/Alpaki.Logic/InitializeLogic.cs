using System.Reflection;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Logic.PipelineBehaviours;
using Alpaki.Logic.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using SystemClock = Microsoft.Extensions.Internal.SystemClock;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services, string privateSecretKey)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(InitializeLogic));
            services.AddMediatR(typeof(InitializeLogic).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddScoped<IInvitationCodesGenerator, InvitationCodesGenerator>();
            services.AddSingleton<IJwtGenerator>(x => new JwtGenerator(privateSecretKey));
            services.AddSingleton<ISystemClock>(new SystemClock());
            return services;
        }
    }
}
