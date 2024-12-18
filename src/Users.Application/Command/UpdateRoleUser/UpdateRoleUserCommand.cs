﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs;
using Users.Application.Responses;
using Users.Domain.Entities;

namespace Users.Application.Command.UpdateRoleUser
{
    public class UpdateRoleUserCommand(Guid userId) : IRequest<Response<string?>>
    {
        public Guid UserId { get; private set; } = userId;
    }
}
