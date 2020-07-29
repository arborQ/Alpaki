using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Invitations.Repositories;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Internal;
using static Alpaki.Logic.Features.Invitations.Exceptions;

namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteerHandler : IRequestHandler<RegisterVolunteer, RegisterVolunteerResponse>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ISystemClock _clock;
        private readonly IInvitationRepository _invitationRepository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterVolunteerHandler(IJwtGenerator jwtGenerator, ISystemClock clock, IInvitationRepository invitationRepository, IVolunteerRepository volunteerRepository, IPasswordHasher passwordHasher)
        {
            _jwtGenerator = jwtGenerator;
            _clock = clock;
            _invitationRepository = invitationRepository;
            _volunteerRepository = volunteerRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterVolunteerResponse> Handle(RegisterVolunteer request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetInvitationAsync(request.Email, cancellationToken);
            
            if(invitation is null)
                throw new InvitationNotFoundException();

            if ( _clock.UtcNow - invitation.CreatedAt > TimeSpan.FromHours(24))
                throw new InvitationHasExpiredException();

            if (invitation.Attempts >= 3)
                throw new InvitationHasBeenRevokedException();

            if (!invitation.Code.ToLower().Equals(request.Code.ToLower()))
            {
                invitation.Attempts += 1;
                await _invitationRepository.UpdateAsync(invitation, cancellationToken);
                throw new InvalidInvitationCodeException();
            }

            if(await _volunteerRepository.ExitsAsync(invitation.Email, cancellationToken))
                throw new VolunteerAlreadyExistsException();

            var passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Brand = request.Brand,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = UserRoleEnum.Volunteer
            };

            invitation.Status = InvitationStateEnum.Accepted;

            await _volunteerRepository.AddAsync(user, cancellationToken);

            var jwtToken = _jwtGenerator.Generate(user);

            return new RegisterVolunteerResponse(user.UserId, jwtToken);
        }
    }
}