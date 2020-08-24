using System;
using System.Collections.Generic;
using System.Text;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;
using Alpaki.Tests.Common.Builders;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.IntegrationTests.Fixtures.Builders
{
    public static class DreamBuilderExtensions
    {
        public static IPostprocessComposer<Dream> DreamBuilder(this Fixture fixture)
        {
            return fixture.Build<Dream>()
                .With(u => u.DreamId, 0)
                .With(u => u.DreamState, DreamStateEnum.Created)
                .Without(u => u.DreamCategory)
                .Without(u => u.DreamCategoryId)
                .Without(u => u.DreamImage)
                .Without(u => u.DreamImageId)
                .Without(u => u.RequiredSteps)
                .Without(u => u.Sponsors)
                .Without(u => u.Volunteers);
        }

        public static IPostprocessComposer<Dream> WithNewCategory(this IPostprocessComposer<Dream> postProcessComposer)
        {
            return postProcessComposer.WithCategory(new Fixture().DreamCategoryBuilder().Create());
        }

        public static IPostprocessComposer<Dream> WithCategory(this IPostprocessComposer<Dream> postProcessComposer, DreamCategory category)
        {
            return postProcessComposer.With(d => d.DreamCategory, category);
        }

        public static IPostprocessComposer<Dream> WithImage(this IPostprocessComposer<Dream> postProcessComposer, int imageSize = 256)
        {
            var fixture = new Fixture();
            var newImage = fixture.ImageBuilder(imageSize).Create();

            return postProcessComposer.With(d => d.DreamImage, newImage);
        }
    }
}
