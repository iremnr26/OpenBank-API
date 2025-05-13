using System;
using NuGet.Common;

namespace OpenBankAPI.Application.Features.Commands.AppUser.LoginUser
{
	public class LoginUserCommandResponse
	{
		
	}

    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public OpenBankAPI.Application.DTOs.Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}

