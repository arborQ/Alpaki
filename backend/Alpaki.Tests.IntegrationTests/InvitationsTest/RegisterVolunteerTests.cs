using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Alpaki.Tests.IntegrationTests.InvitationsTest
{
    public class RegisterVolunteerFake
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Brand { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }

    public class RegisterVolunteerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        private readonly GraphQLClient _graphQL;
        private readonly Fixture _fixture;
        private readonly IServiceProvider _services;
        public RegisterVolunteerTests(CustomWebApplicationFactory factory, ITestOutputHelper testOutputHelper)
        {
            _services = factory.Services;
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient();
            _graphQL = new GraphQLClient(_client);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Basic_scenario()
        {
            using (var scope = _services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IDatabaseContext>();
                await db.Invitations.AddAsync(
                    new Invitation
                    {
                        Email = "test@test.com",
                        Status = InvitationStateEnum.Pending,
                        Code = "A3B5",
                        Timestamp = DateTimeOffset.UtcNow,
                    }
                );
                await db.SaveChangesAsync();
            }
            
            var (fake, request) = _fixture
                .Build<RegisterVolunteerFake>()
                .With(x=>x.FirstName,"test")
                .With(x=>x.LastName,"test")
                .With(x=>x.PhoneNumber,"123456789")
                .With(x=>x.Brand,"Kato")
                .With(x => x.Email, "test@test.com")
                .With(x=>x.Code,"A3B5")
                .With(x=>x.Password,"Test1234")
                .Create()
                .WithJsonContent();

            var response = await _client.PostAsync("/api/volunteers", request);
            _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            var body = await response.ReadAs<RegisterVolunteerResponse>();
            body.Should().NotBeNull();
            body.Token.Should().NotBeNullOrWhiteSpace();
            body.UserId.Should().NotBe(0);
        }
    }
}