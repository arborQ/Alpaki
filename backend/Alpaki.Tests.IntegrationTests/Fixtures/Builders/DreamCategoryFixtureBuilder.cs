using Alpaki.Database.Models;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.IntegrationTests.Fixtures.Builders
{
    public static class DreamCategoryFixtureBuilder
    {
        public static IPostprocessComposer<DreamCategory> DreamCategoryBuilder(this Fixture fixture)
        {
            return fixture.Build<DreamCategory>()
                .With(u => u.DreamCategoryId, 0)
                .Without(u => u.Dreams);
        }
    }
}
