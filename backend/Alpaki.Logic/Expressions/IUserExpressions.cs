using System;
using System.Linq.Expressions;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Expressions
{
    public interface IUserExpressions
    {
        Expression<Func<User, bool>> UserQuery { get; }
    }
}
