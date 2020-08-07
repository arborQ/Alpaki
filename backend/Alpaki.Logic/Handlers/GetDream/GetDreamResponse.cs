using System;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamResponse
    {
        public long DreamId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public string DreamUrl { get; set; }

        public string Tags { get; set; }

        internal static Expression<Func<Dream, GetDreamResponse>> DreamToDreamListItemMapper = dream => new GetDreamResponse
        {
            DreamId = dream.DreamId,
            Age = dream.Age,
            DreamUrl = dream.DreamUrl,
            FirstName = dream.FirstName,
            LastName = dream.LastName,
            Gender = dream.Gender,
            Tags = dream.Tags
        };
    }
}
