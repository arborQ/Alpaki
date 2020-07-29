using Alpaki.Database.Models;
using AutoFixture;
using AutoFixture.Dsl;
using Microsoft.AspNet.Identity;

namespace Alpaki.Tests.IntegrationTests.Fixtures.Builders
{
    public static class VolunteerFixtureBuilder
    {
        public static IPostprocessComposer<User> UserBuilder(this Fixture fixture)
        {
            return fixture.Build<User>()
                .With(u => u.UserId, 0)
                .Without(u => u.AssignedDreams);
        }

        public static IPostprocessComposer<User> VolunteerBuilder(this Fixture fixture)
        {
            return fixture.UserBuilder()
                .With(u => u.Role, CrossCutting.Enums.UserRoleEnum.Volunteer);
        }

        public static IPostprocessComposer<User> CoordinatorBuilder(this Fixture fixture)
        {
            return fixture.UserBuilder()
                .With(u => u.Role, CrossCutting.Enums.UserRoleEnum.Coordinator);
        }

        public static IPostprocessComposer<User> AdminBuilder(this Fixture fixture)
        {
            return fixture.UserBuilder()
                .With(u => u.Role, CrossCutting.Enums.UserRoleEnum.Admin);
        }

        public static IPostprocessComposer<User> WithPassword(this IPostprocessComposer<User> postProcessComposer, string password)
        {
            var hasher = new PasswordHasher();
            return postProcessComposer.With(u => u.PasswordHash, hasher.HashPassword(password));
        }
    }
}
