﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Authentication;
using Users.Application.Responses;
using Users.Application.Responses.Messages;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.Application.Command.LoginUser
{
    public class LoginUserHandler(IAuthenticationService authenticationService, IUserRepository userRepository) : IRequestHandler<LoginUserCommand, Response<string?>>
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Response<string?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authenticationService.ComputeSha256Hash(request.Password);
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);
            if (user is null)
                return new Response<string?>(null, 404, ResponseMessages.USER_NOT_FOUND.GetDescription());

            string jwt = _authenticationService.GenerateJwtToken(user);
            user.UpdateLastLoginDate();

            return new Response<string?>(jwt, 200, ResponseMessages.LOGIN_SUCCESS.GetDescription());
        }
    }
}
