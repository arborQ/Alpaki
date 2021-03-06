﻿using Alpaki.CrossCutting.Enums;
using MediatR;

namespace Alpaki.Logic.Handlers.ChangeUserRole
{
    public class ChangeUserRoleRequest : IRequest<ChangeUserRoleResponse>
    {
        public long UserId { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}