using Alpaki.Database.Models;
using GraphQL.Types;

namespace Alpaki.WebApi.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(u => u.UserId);
            Field(u => u.Email);
            Field(u => u.FirstName);
            Field(u => u.LastName);
            Field(u => u.Brand);
            Field(u => u.PhoneNumber);
        }
    }
}
