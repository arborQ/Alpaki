﻿using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database;
using Alpaki.Database.Models;
using Alpaki.Database.Models.Invitations;

namespace Alpaki.WebApi.GraphQL
{
    public class AdminDreamerQuery : DreamerQuery
    {
        private readonly IDatabaseContext _dreamsDatabaseContext;
        private readonly IDatabaseContext _usersDatabaseContext;
        private readonly IDatabaseContext _invitationsDatabaseContext;
        private readonly IDatabaseContext _categoriesDatabaseContext;

        public AdminDreamerQuery(IDatabaseContext dreamsDatabaseContext, IDatabaseContext usersDatabaseContext, IDatabaseContext invitationsDatabaseContext, IDatabaseContext categoriesDatabaseContext)
        {
            _dreamsDatabaseContext = dreamsDatabaseContext;
            _usersDatabaseContext = usersDatabaseContext;
            _invitationsDatabaseContext = invitationsDatabaseContext;
            _categoriesDatabaseContext = categoriesDatabaseContext;
        }

        protected override IQueryable<Dream> QueryDreams()
        {
            return _dreamsDatabaseContext.Dreams;
        }

        protected override IQueryable<User> QueryUsers()
        {
            return _usersDatabaseContext.Users;
        }

        protected override IQueryable<Invitation> QueryInvitations()
        {
            return _invitationsDatabaseContext.Invitations.Where(x=>x.Status == InvitationStateEnum.Pending);
        }

        protected override IQueryable<DreamCategory> QueryDreamCategories()
        {
            return _categoriesDatabaseContext.DreamCategories;
        }
    }
}
