using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.AuthorizeUserPassword
{
    public class AuthorizeUserPasswordHandler : IRequestHandler<AuthorizeUserPasswordRequest, AuthorizeUserPasswordResponse>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IDatabaseContext _databaseContext;

        public AuthorizeUserPasswordHandler(IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator, IDatabaseContext databaseContext)
        {
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _databaseContext = databaseContext;
        }

        public async Task<AuthorizeUserPasswordResponse> Handle(AuthorizeUserPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _databaseContext.Users.Where(u => u.Email == request.Login).SingleAsync();

            var passwordHash = _passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);

            if (passwordHash == PasswordVerificationResult.Failed)
            {
                throw new InvalidPasswordException();
            }

            var token = _jwtGenerator.Generate(user);

            return new AuthorizeUserPasswordResponse { Token = token };
        }
    }
}
