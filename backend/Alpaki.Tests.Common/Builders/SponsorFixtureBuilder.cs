using System;
using Alpaki.Database.Models;
using AutoFixture;
using AutoFixture.Dsl;

namespace Alpaki.Tests.Common.Builders
{
    public static class SponsorFixtureBuilder
    {
        public static IPostprocessComposer<Sponsor> SponsorBuilder(this Fixture fixture)
        {
            return fixture.Build<Sponsor>()
                .Without(s => s.SponsorId)
                .Without(s => s.Dreams)
                .With(s => s.Email, () => $"{Guid.NewGuid()}@alpaki.pl");
        }
    }
}
