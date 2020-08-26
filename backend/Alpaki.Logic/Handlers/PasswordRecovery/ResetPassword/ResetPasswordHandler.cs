using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Handlers.PasswordRecovery.ResetPassword
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ResetPasswordHandler(IDatabaseContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);
            
            if(user is null)
                throw new ResetPasswordException();

            var result = VerifyToken(user, request.Token);
            if(result == false)
                throw new ResetPasswordException();
            
            var passwordHash = _passwordHasher.HashPassword(null, request.Password);

            user.PasswordHash = passwordHash;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResetPasswordResponse();
        }

        private bool VerifyToken(User user, string token)
        {
            var key = Encoding.ASCII.GetBytes(user.PasswordHash);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                },out _);
                return claims.Identity.Name == user.UserId.ToString();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}