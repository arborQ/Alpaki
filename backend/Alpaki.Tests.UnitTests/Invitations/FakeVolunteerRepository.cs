using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database.Models;
using Alpaki.Logic.Features.Invitations.RegisterVolunteer;
using Alpaki.Logic.Features.Invitations.Repositories;

namespace Alpaki.Tests.UnitTests.Invitations
{
    public class FakeVolunteerRepository : IVolunteerRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _idCounter = 0;

        public User Get(long id)
        {
            var user = _users.FirstOrDefault(x => x.UserId == id);
            return new User
            {
                UserId = user.UserId,
                PhoneNumber = user.PhoneNumber,
                Brand = user.Brand,
                LastName = user.LastName,
                Role = user.Role,
                FirstName = user.FirstName,
                PasswordHash = user.PasswordHash,
                AssignedDreams = user.AssignedDreams,
                Email = user.Email
            };
        }

        public Task AddAsync(User user, CancellationToken cancellationToken)
        {
            user.UserId = Next();
            _users.Add(user);
            return Task.CompletedTask; ;
        }

        public Task<bool> ExitsAsync(string email, CancellationToken cancellationToken)
        {
            return Task.FromResult(_users.Any(x => x.Email.ToLower() == email.ToLower()));
        }
        private int Next() => ++_idCounter;

    }
}