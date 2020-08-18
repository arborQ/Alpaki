using System.Reflection;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Logic.Features.Invitations.Repositories;
using Alpaki.Logic.Mails;
using Alpaki.Logic.PipelineBehaviours;
using Alpaki.Logic.Services;
using Alpaki.Logic.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;

namespace Alpaki.Logic
{
    public static class InitializeLogic
    {
        public static IServiceCollection RegisterLogicServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(InitializeLogic));
            services.AddMediatR(typeof(InitializeLogic).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddScoped<IInvitationCodesGenerator, InvitationCodesGenerator>();
            services.AddSingleton<IJwtGenerator, JwtGenerator>();
            services.AddSingleton<ISystemClock>(new SystemClock());
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IUserScopedDatabaseReadContext, UserScopedDatabaseReadContext>();
            services.AddTransient<IImageIdValidator, ImageIdValidator>();
            services.AddMailKit();
          
            return services;
        }
    }
}
