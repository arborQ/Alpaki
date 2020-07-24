using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using static Alpaki.Logic.Features.Invitations.Exceptions;

namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteerHandler : IRequestHandler<RegisterVolunteer, RegisterVolunteerResponse>
    {
        private readonly IDatabaseContext _dBContext;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ISystemClock _clock;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterVolunteerHandler(IDatabaseContext dBContext, IJwtGenerator jwtGenerator, ISystemClock clock)
        {
            _dBContext = dBContext;
            _jwtGenerator = jwtGenerator;
            _clock = clock;
            _passwordHasher = new PasswordHasher();
        }
        public async Task<RegisterVolunteerResponse> Handle(RegisterVolunteer request, CancellationToken cancellationToken)
        {
            var invitation = await _dBContext.Invitations.SingleOrDefaultAsync(
                x => x.Email.ToLower().Equals(request.Email.ToLower())
                     && x.Status == InvitationStateEnum.Pending,
                cancellationToken
            );

            if (invitation is null)
                throw new InvitationNotFoundException();

            if (_clock.UtcNow - invitation.CreatedAt > TimeSpan.FromHours(24))
                throw new InvitationHasExpiredException();

            if (invitation.Attempts >= 3)
                throw new InvitationHasBeenRevokedException();

            if (!invitation.Code.ToLower().Equals(request.Code.ToLower()))
            {
                invitation.Attempts += 1;
                await _dBContext.SaveChangesAsync(cancellationToken);
                throw new InvalidInvitationCodeException();
            }

            if (await _dBContext.Users.AnyAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()), cancellationToken: cancellationToken))
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

            await _dBContext.Users.AddAsync(user, cancellationToken);
            await _dBContext.SaveChangesAsync(cancellationToken);

            var jwtToken = _jwtGenerator.Generate(user);

            return new RegisterVolunteerResponse(user.UserId, jwtToken);
        }
    }
}