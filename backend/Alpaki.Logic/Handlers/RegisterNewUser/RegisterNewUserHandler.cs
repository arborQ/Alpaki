using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Alpaki.Logic.Handlers.RegisterNewUser
{
    public class RegisterNewUserHandler : IRequestHandler<RegisterNewUserRequest, RegisterNewUserResponse>
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ILogger<RegisterNewUserHandler> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterNewUserHandler(IDatabaseContext databaseContext, ILogger<RegisterNewUserHandler> logger, IPasswordHasher<User> passwordHasher)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterNewUserResponse> Handle(RegisterNewUserRequest request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Brand = request.Brand,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Role = CrossCutting.Enums.UserRoleEnum.None,
                PasswordHash = _passwordHasher.HashPassword(null, request.Password),
                ProfileImageId = request.ProfileImageId
            };

            await _databaseContext.Users.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            _logger.LogInformation($"New user created, Email: [{newUser.Email}], UserId: [{newUser.UserId}]");

            var response = new RegisterNewUserResponse { UserId = newUser.UserId };

            return response;
        }
    }
}
