using System;
using System.Threading;
using System.Threading.Tasks;
using Alpaki.Moto.Database;
using MediatR;

namespace Alpaki.Logic.Handlers.DeleteBrand
{
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandRequest, DeleteBrandResponse>
    {
        private readonly IMotoDatabaseContext _databaseContext;

        public DeleteBrandHandler(IMotoDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<DeleteBrandResponse> Handle(DeleteBrandRequest request, CancellationToken cancellationToken)
        {
            var response = new DeleteBrandResponse();
            
            throw new NotImplementedException();
        }
    }
}
