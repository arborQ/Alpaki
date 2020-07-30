using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Features.Dreamer.CreateDreamer
{
    public class CreateDreamerRequest : IRequest<CreateDreamerResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Tags { get; set; }

        public GenderEnum Gender { get; set; }

        public string DreamUrl { get; set; }

        public long CategoryId { get; set; }

        public bool IsSponsorRequired { get; set; } = true;
    }
}
