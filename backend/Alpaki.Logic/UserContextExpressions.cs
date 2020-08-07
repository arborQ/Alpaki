using System;
using System.Data.Entity;
using System.Linq;
using Alpaki.CrossCutting.Enums;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database;
using Alpaki.Database.Models;

namespace Alpaki.Logic
{
    public interface IUserScopedDatabaseReadContext
    {
        public IQueryable<Dream> Dreams { get; }

        public IQueryable<User> Users { get; }
    }

    public class UserScopedDatabaseReadContext : IUserScopedDatabaseReadContext
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly ICurrentUserService _currentUserService;

        public UserScopedDatabaseReadContext(IDatabaseContext databaseContext, ICurrentUserService currentUserService)
        {
            _databaseContext = databaseContext;
            _currentUserService = currentUserService;
        }

        public IQueryable<Dream> Dreams
        {
            get
            {
                var dreams = _databaseContext.Dreams.AsNoTracking();

                if (_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Coordinator))
                {
                    return dreams;
                }

                return dreams.Where(d => d.Volunteers.Any(v => v.VolunteerId == _currentUserService.CurrentUserId));
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                var users = _databaseContext.Users.AsNoTracking();

                if (_currentUserService.CurrentUserRole.HasFlag(UserRoleEnum.Coordinator))
                {
                    return users;
                }

                var currentUserId = _currentUserService.CurrentUserId;
                var dreamIds = _databaseContext.AssignedDreams.AsNoTracking().Where(ad => ad.VolunteerId == currentUserId).Select(ad => ad.DreamId);

                return users.Where(u => u.UserId == currentUserId || u.AssignedDreams.Any(ad => dreamIds.Contains(ad.DreamId)));
            }
        }
    }
}
