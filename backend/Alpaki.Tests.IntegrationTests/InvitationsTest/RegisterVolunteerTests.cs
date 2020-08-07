using System;
using System.Net.Http;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models.Invitations;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Xunit;

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

    public class RegisterVolunteerTests : IntegrationTestsClass
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;
        private readonly IDatabaseContext _dbContext;
        public RegisterVolunteerTests(IntegrationTestsFixture integrationTestsFixture):base(integrationTestsFixture)
        {
            _dbContext = integrationTestsFixture.DatabaseContext;
            _client = integrationTestsFixture.ServerClient;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task returns_correct_successful_response()
        {
            await _dbContext.Invitations.AddAsync(
                new Invitation
                {
                    Email = "test@test.com",
                    Status = InvitationStateEnum.Pending,
                    Code = "A3B5",
                    CreatedAt = DateTimeOffset.UtcNow,
                }
            );
            await _dbContext.SaveChangesAsync();

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
            response.EnsureSuccessStatusCode();
            var body = await response.ReadAs<RegisterVolunteerResponse>();
            body.Should().NotBeNull();
            body.Token.Should().NotBeNullOrWhiteSpace();
            body.UserId.Should().NotBe(0);
        }
    }
}