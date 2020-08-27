using System;
using System.Linq.Expressions;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamResponse
    {
        public long DreamId { get; set; }

        public string Title { get; set; }

        public string DisplayName { get; set; }

        public int Age { get; set; }

        public string DreamUrl { get; set; }

        public string Tags { get; set; }

        public string DreamImageUrl { get; set; }

        public string CityName { get; set; }


        internal static Expression<Func<Dream, GetDreamResponse>> DreamToDreamListItemMapper = dream => new GetDreamResponse
        {
            DreamId = dream.DreamId,
            Title = dream.Title,
            Age = dream.Age,
            DreamUrl = dream.DreamUrl,
            DisplayName = dream.DisplayName,
            Tags = dream.Tags,
            CityName = dream.CityName,
            DreamImageUrl = dream.DreamImageId.HasValue ? $"/api/images/{dream.DreamImageId}.png" : null,
        };
    }
}
