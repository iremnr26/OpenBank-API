using System;
namespace OpenBankAPI.Application.Helpers
{
	public static class AccountHelper
	{
           public static string GenerateAccountNumber()
            {
                Random random = new Random();
                return $"{random.Next(10000000, 99999999)}{random.Next(10000000, 99999999)}";
            }
        
    }
}

