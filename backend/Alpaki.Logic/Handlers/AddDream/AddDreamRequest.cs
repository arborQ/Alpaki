using System;
using MediatR;

namespace Alpaki.Logic.Handlers.AddDream
{
    public class AddDreamRequest : IRequest<AddDreamResponse>
    {
        public string DisplayName { get; set; }

        public int Age { get; set; }

        public string Tags { get; set; }

        public string DreamUrl { get; set; }

        public long CategoryId { get; set; }

        public bool IsSponsorRequired { get; set; } = true;

        public Guid? DreamImageId { get; set; }

        public long[] VolunteerIds { get; set; }
    }
}
