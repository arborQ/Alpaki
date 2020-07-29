using System.Collections.Generic;
using System.Linq;
using Alpaki.Database.Models;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.Common.Builders
{
    public static class DreamCategoryFixtureBuilder
    {
        public static IPostprocessComposer<DreamCategory> DreamCategoryBuilder(this Fixture fixture, IReadOnlyCollection<DreamCategoryDefaultStep> steps)
        {
            return fixture.Build<DreamCategory>()
                .With(u => u.DreamCategoryId, 0)
                .With(u => u.DefaultSteps, steps.ToList())
                .Without(u => u.Dreams);
        }

        public static IPostprocessComposer<DreamCategory> DreamCategoryBuilder(this Fixture fixture, int stepCount = 10)
        {
            return fixture.DreamCategoryBuilder(fixture.DreamCategoryDefaultStepBuilder().CreateMany(stepCount).ToList());
        }

        public static IPostprocessComposer<DreamCategoryDefaultStep> DreamCategoryDefaultStepBuilder(this Fixture fixture)
        {
            return fixture.Build<DreamCategoryDefaultStep>().Without(c => c.DreamCategory);
        }
    }
}
