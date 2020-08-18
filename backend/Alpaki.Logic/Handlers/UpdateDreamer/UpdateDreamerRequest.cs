using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.UpdateDreamer
{
    public class UpdateDreamerRequest : IRequest<UpdateDreamerResponse>
    {
        public long DreamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public GenderEnum? Gender { get; set; }
        public string DreamUrl { get; set; }
        public string Tags { get; set; }
        public long? DreamCategoryId { get; set; }
    }
}