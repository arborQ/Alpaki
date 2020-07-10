using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Services
{
    public class UserAuthorizeHandler : IRequestHandler<UserAuthorizeRequest, UserAuthorizeResponse>
    {
        private readonly IValidator<UserAuthorizeRequest> _validator;
        private readonly IDatabaseContext _databaseContext;

        public UserAuthorizeHandler(IValidator<UserAuthorizeRequest> validator, IDatabaseContext databaseContext)
        {
            _validator = validator;
            _databaseContext = databaseContext;
        }

        public async Task<UserAuthorizeResponse> Handle(UserAuthorizeRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception("invalid request");
            }

            var user = await _databaseContext.Users.Where(u => u.Email == request.Login).FirstOrDefaultAsync(cancellationToken);

            return new UserAuthorizeResponse();
        }

        //private string generateJwtToken(User user)
        //{
        //    // generate token that is valid for 7 days
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Id.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
