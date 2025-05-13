using System;
using Domain.Entities;

namespace OpenBankAPI.Application.Abstraction
{
	public interface IAccountService
	{
        List<Account> GetAccounts(); // Tüm kullanıcıları getir
        Account GetAccountById(long id); // Belirli bir kullanıcıyı getir
    }
}

