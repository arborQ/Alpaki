using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Logic.MailKit.Builders;
using Alpaki.Logic.Mails;
using Alpaki.Logic.Mails.Templates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Alpaki.Logic.Handlers.PasswordRecovery.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMailService _mailService;
        private readonly ISystemClock _systemClock;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly MailKitOptions _options;

        public ForgotPasswordHandler(IDatabaseContext dbContext, 
            IMailService mailService, 
            IOptions<MailKitOptions> options,
            ISystemClock systemClock,
            LinkGenerator linkGenerator,
            IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _systemClock = systemClock;
            _linkGenerator = linkGenerator;
            _contextAccessor = contextAccessor;
            _options = options.Value;
        }
        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken: cancellationToken);
            if (user == null)
            {
                return new ForgotPasswordResponse();
            }

            var token = GetToken(user);

            var link = GenerateLink(user, token);
            
            var message = MessageBuilder
                .Create()
                .WithReceiver(user.Email)
                .WithSender(_options.Email)
                .WithSubject(MessageTemplates.PasswordReset.Subject)
                .WithBody(MessageTemplates.PasswordReset.Body, link)
                .Build();

            await _mailService.Send(message);

            return new ForgotPasswordResponse();
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(user.PasswordHash);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, ((int)user.Role).ToString()), 
                }),
                Expires = _systemClock.UtcNow.AddHours(1).UtcDateTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateLink(IUser user, string token)
        {
            return _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, "/PasswordReset",null, new {userId = user.UserId, token=token});
        }
    }
}