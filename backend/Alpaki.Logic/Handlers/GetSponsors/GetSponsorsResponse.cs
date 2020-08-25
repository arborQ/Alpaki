using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetSponsors
{
    public class GetSponsorsResponse
    {
        public IReadOnlyCollection<SponsorItem> Sponsors { get; set; }

        internal static Expression<Func<Sponsor, SponsorItem>> MapSponsorToSponsorItem = sponsor => new SponsorItem
        {
            SponsorId = sponsor.SponsorId,
            DisplayName = sponsor.DisplayName,
            ContactPersonName = sponsor.ContactPersonName,
            Email = sponsor.Email,
            Brand = sponsor.Brand,
            CooperationType = sponsor.CooperationType, 
            DreamCount = sponsor.Dreams.Count
        };

        public class SponsorItem
        {
            public long SponsorId { get; set; }

            public string DisplayName { get; set; }

            public string ContactPersonName { get; set; }

            public string Email { get; set; }

            public string Brand { get; set; }

            public int DreamCount { get; set; }

            public SponsorTypeEnum CooperationType { get; set; }
        }
    }
}
