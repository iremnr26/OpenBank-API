using System;
namespace OpenBankAPI.Application.Abstraction.Services
{
	public interface IExternalAuthentication
	{
       Task<DTOs.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
       Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
    }
}

