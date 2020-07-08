using System;
using System.Linq;
using System.Linq.Expressions;
using Alpaki.CrossCutting.Interfaces;
using Alpaki.Database.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Alpaki.Logic.Expressions
{
    public class VolontierDreamerExpressions : IDreamersExpressions
    {
        private readonly ICurrentUserService _currentUserService;

        public VolontierDreamerExpressions(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public Expression<Func<Dreamer, bool>> DreamersQuery
        {
            get
            {
                return dreamer => true;
                //return dreamer => dreamer.Dreams.Where(d => d.Volunteers.Where(v => v.VolunteerId == _currentUserService.CurrentUserId).Any()).Any();
            }
        }
    }
}
