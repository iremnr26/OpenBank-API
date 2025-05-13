using System;
namespace OpenBankAPI.Application.ViewModels.Accounts
{
	public class VM_Update_Account
	{
        public string Id { get; set; } // Hesap numarasıyla

        public decimal Balance { get; set; } // Hesap bakiyesii

        public string Currency { get; set; } // Para birimini güncelleme 


    }
}

