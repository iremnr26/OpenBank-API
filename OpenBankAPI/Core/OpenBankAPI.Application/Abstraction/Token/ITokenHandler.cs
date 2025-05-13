using System;
using Domain.Entities.Identity;

namespace OpenBankAPI.Application.Abstraction.Token
{
	public interface ITokenHandler
	{
      DTOs.Token CreateAccessToken(int second, AppUser appUser) ;
      string CreateRefreshToken();
    }
}

