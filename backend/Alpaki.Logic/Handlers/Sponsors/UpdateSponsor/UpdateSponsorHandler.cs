using System.Threading;
using System.Threading.Tasks;
using Alpaki.Database;
using Alpaki.Database.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Alpaki.Logic.Handlers.Sponsors.UpdateSponsor
{
    public class UpdateSponsorHandler : IRequestHandler<UpdateSponsorRequest,UpdateSponsorResponse>
    {
        private readonly IDatabaseContext _dbContext;

        public UpdateSponsorHandler(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UpdateSponsorResponse> Handle(UpdateSponsorRequest request, CancellationToken cancellationToken)
        {
            var sponsor = await _dbContext.Sponsors.FindAsync(request.Id);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                sponsor.Name = request.Name;
            }
            
            if (!string.IsNullOrWhiteSpace(request.ContactPerson))
            {
                sponsor.ContactPerson = request.ContactPerson;
            }
            
            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                sponsor.PhoneNumber = request.PhoneNumber;
            }
            
            if (!string.IsNullOrWhiteSpace(request.Mail))
            {
                sponsor.Mail = request.Mail;
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new UpdateSponsorResponse();
        }
    }
}