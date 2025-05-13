using System;
using OpenBankAPI.Application.Abstraction.Services;

namespace OpenBankAPI.Application.Abstraction
{
    public interface IAuthService: IExternalAuthentication, IInternalAuthentication
    {
        Task PasswordResetAsnyc(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}

