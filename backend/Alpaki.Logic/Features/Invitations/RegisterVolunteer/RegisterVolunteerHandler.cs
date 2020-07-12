using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.AspNet.Identity;

namespace Alpaki.Logic.Features.Invitations.RegisterVolunteer
{
    public class RegisterVolunteerHandler : IRequestHandler<RegisterVolunteer, RegisterVolunteerResponse>
    {
        private readonly IDatabaseContext _dBContext;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterVolunteerHandler(IDatabaseContext dBContext)
        {
            _dBContext = dBContext;
            _passwordHasher = new PasswordHasher();
        }
        public async Task<RegisterVolunteerResponse> Handle(RegisterVolunteer request, CancellationToken cancellationToken)
        {
            var invitation = _dBContext.Invitations.SingleOrDefault(
                x => x.Email.ToLower().Equals(request.Email.ToLower())
                     && x.Status == InvitationStateEnum.Pending
            );
            
            if(invitation is null)
                throw new Exception("Not valid invitation was found");

            if ((invitation.Timestamp - DateTimeOffset.UtcNow) > TimeSpan.FromHours(24))
                throw new Exception("Invitation has expired");

            if (invitation.Attempts >= 3)
            {
                throw new Exception("Code is no longer valid");
            }

            if (!invitation.Code.ToLower().Equals(request.Code.ToLower()))
            {
                //TODO decrease number of tries
                invitation.Attempts += 1;
                await _dBContext.SaveChangesAsync(cancellationToken);
                throw new Exception("Invalid code");
            }

            if(request.Password.Length < 8)
                throw new Exception("Given password is not matching rules");

            if(_dBContext.Users.Any(x => x.Email.ToLower().Equals(request.Email.ToLower())))
                throw new Exception("There is already volunteer with given email.");

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

            //generate jwt token

            return new RegisterVolunteerResponse(user.UserId, "");
        }
    }
}