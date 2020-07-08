using System;

namespace Alpaki.CrossCutting.Enums
{
    [Flags]
    public enum UserRoleEnum
    {
        None = 0,
        Volunteer = 1,
        Coordinator = 2 | Volunteer, // 3
        Admin = 4 | Coordinator // 7
    }
}
