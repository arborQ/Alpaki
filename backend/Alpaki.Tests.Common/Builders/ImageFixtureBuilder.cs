using System;
using System.Linq;
using Alpaki.Database.Models;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.Common.Builders
{
    public static class ImageFixtureBuilder
    {
        public static IPostprocessComposer<Image> ImageBuilder(this Fixture fixture, int imageSize = 256)
        {
            return fixture.Build<Image>()
                .With(u => u.ImageId, Guid.Empty)
                .With(u => u.ImageData, fixture.CreateMany<byte>(imageSize).ToArray());
        }
    }
}
