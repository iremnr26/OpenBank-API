using System;
using MediatR;

namespace OpenBankAPI.Application.Features.Commands.AppUser.CreateUser
{
	public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
	{

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; } // IdentityUser.UserName
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
    }
}

