using System;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Expressions
{
    public class VolontierUserExpressions : IUserExpressions
    {
        private readonly ICurrentUserService _currentUserService;

        public VolontierUserExpressions(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Expression<Func<User, bool>> UserQuery
        {
            get
            {
                return user => user.UserId == _currentUserService.CurrentUserId;
            }
        }
    }
}
