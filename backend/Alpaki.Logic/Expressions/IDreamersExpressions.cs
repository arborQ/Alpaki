using System;
using System.Linq.Expressions;
using Alpaki.Database.Models;

namespace Alpaki.Logic.Expressions
{
    public interface IDreamersExpressions
    {
        Expression<Func<Dreamer, bool>> DreamersQuery { get; }
    }
}
