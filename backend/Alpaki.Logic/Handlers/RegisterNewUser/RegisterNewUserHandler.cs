using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using MediatR;

namespace Alpaki.Logic.Handlers.RegisterNewUser
{
    public class RegisterNewUserHandler : IRequestHandler<RegisterNewUserRequest, RegisterNewUserResponse>
    {
        private readonly IDatabaseContext _databaseContext;

        public RegisterNewUserHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<RegisterNewUserResponse> Handle(RegisterNewUserRequest request, CancellationToken cancellationToken)
        {
            var response = new RegisterNewUserResponse();
            
            return await Task.FromResult(response);
        }
    }
}
