using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetDreams
{
    public class GetDreamsResponse
    {
        public IReadOnlyCollection<DreamListItem> Dreams { get; set; }

        public class DreamListItem
        {
            public long DreamId { get; set; }

            public string DisplayName { get; set; }

            public int Age { get; set; }

            public string DreamUrl { get; set; }

            public string DreamImageUrl { get; set; }

            public string Tags { get; set; }

            public DreamCategoryItem DreamCategory { get; set; }

            internal static Expression<Func<Dream, DreamListItem>> DreamToDreamListItemMapper = dream => new DreamListItem
            {
                DreamId = dream.DreamId,
                Age = dream.Age,
                DreamUrl = dream.DreamUrl,
                DisplayName = dream.DisplayName,
                Tags = dream.Tags,
                DreamCategory = new DreamCategoryItem
                {
                    DreamCategoryId = dream.DreamCategory.DreamCategoryId,
                    DreamCategoryName = dream.DreamCategory.CategoryName
                }
            };
        }

        public class DreamCategoryItem
        {
            public long DreamCategoryId { get; set; }
            public string DreamCategoryName { get; set; }
        }
    }
}
