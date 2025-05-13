using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Application.DTOs.User;
using OpenBankAPI.Application.Exceptions;

namespace OpenBankAPI.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
  {
        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
      {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Name = request.Name,
                LastName= request.Lastname,
                Password = request.Password,
                ConfirmPass = request.ConfirmPass,
                Username = request.Username,
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded,
            };

            throw new UserCreateFailedException();
       }


     }
 }

