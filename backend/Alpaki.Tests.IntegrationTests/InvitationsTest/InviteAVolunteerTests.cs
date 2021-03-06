﻿using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Logic.Features.Invitations.InviteAVolunteer;
using Alpaki.Tests.IntegrationTests.Extensions.ControllerExtensions;
using Alpaki.Tests.IntegrationTests.Fixtures;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Alpaki.Tests.IntegrationTests.InvitationsTest
{
    public class InviteAVolunteerFake
    {
        public string Email { get; set; }
    }

    public class InvitationResponse
    {
        public InvitationItem[] Invitations { get; set; }
        public class InvitationItem
        {
            public int InvitationId { get; set; }
            public string Email { get; set; }
            public string Code { get; set; }
            public InvitationStateEnum Status { get; set; }
        }
    }
    public class InviteAVolunteerTests : IntegrationTestsClass
    {
        private readonly HttpClient _client;
        private readonly Fixture _fixture;

        public InviteAVolunteerTests(IntegrationTestsFixture integrationTestsFixture) : base(integrationTestsFixture)
        {
            _client = integrationTestsFixture.ServerClient;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task returns_correct_successful_response()
        {
            // Arrange
            IntegrationTestsFixture.SetUserAdminContext();
            var (fake, request) = _fixture
                .Build<InviteAVolunteerFake>()
                .With(
                    x => x.Email,
                    _fixture.Create<MailAddress>().Address
                )
                .Create()
                .WithJsonContent();

            // Act
            var response = await _client.PostAsync("/api/invitations", request);
            response.EnsureSuccessStatusCode();
            var body = await response.ReadAs<InviteAVolunteerResponse>();
            body.Should().NotBeNull();
            body.InvitationId.Should().NotBe(0);
            body.InvitationCode.Should().NotBeNullOrWhiteSpace();

            var responseObject = await _client.GetInvitations();

            // Assert
            responseObject.Should().NotBeNull();
            responseObject.Invitations.Should().SatisfyRespectively(
                i =>
                {
                    i.Email.ToLowerInvariant().Should().BeEquivalentTo(fake.Email.ToLowerInvariant());
                    i.InvitationId.Should().NotBe(0);
                    i.Code.Should().NotBeNullOrWhiteSpace().And.MatchRegex("[0-9A-Z]{4}");
                    i.Status.Should().BeEquivalentTo(InvitationStateEnum.Pending);
                }
            );
        }
    }
}