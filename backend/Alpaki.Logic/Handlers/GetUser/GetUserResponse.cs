using System;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Enums;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Handlers.GetUser
{
    public class GetUserResponse
    {
        public long UserId { get; set; }
        
        public UserRoleEnum UserRole { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Brand { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfileImageUrl { get; set; }

        internal static Expression<Func<User, GetUserResponse>> UserToGetUserResponseMapper = user => new GetUserResponse
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserRole = user.Role,
            Email = user.Email,
            Brand = user.Brand,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageId.HasValue ? $"/api/images/{user.ProfileImageId}.png" : null
        };
    }
}
