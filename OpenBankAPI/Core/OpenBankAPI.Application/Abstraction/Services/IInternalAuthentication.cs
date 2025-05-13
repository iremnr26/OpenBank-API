using System;
namespace OpenBankAPI.Application.Abstraction.Services
{
	public interface IInternalAuthentication
	{
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}

