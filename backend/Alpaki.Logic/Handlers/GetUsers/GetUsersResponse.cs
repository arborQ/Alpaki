using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetUsers
{
    public class GetUsersResponse
    {
        public IReadOnlyCollection<UserListItem> Users { get; set; }

        public class UserListItem
        {
            public long UserId { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Brand { get; set; }

            public string PhoneNumber { get; set; }

            internal static Expression<Func<User, UserListItem>> UserToUserListItemMapper = user => new UserListItem
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Brand = user.Brand,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
