using System;
using MediatR;

namespace Alpaki.Logic.Handlers.UpdateDream
{
    public class UpdateDreamRequest : IRequest<UpdateDreamResponse>
    {
        public long DreamId { get; set; }

        public string DisplayName { get; set; }

        public int? Age { get; set; }

        public string DreamUrl { get; set; }

        public string Tags { get; set; }

        public long? CategoryId { get; set; }

        public Guid? DreamImageId { get; set; }

        public long[] VolunteerIds { get; set; }

        public long[] SponsorIds { get; set; }

    }
}