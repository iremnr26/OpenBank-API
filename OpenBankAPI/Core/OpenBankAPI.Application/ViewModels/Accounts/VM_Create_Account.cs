using System;
using Domain.Entities;

namespace OpenBankAPI.Application.ViewModels.Accounts
{
	public class VM_Create_Account
	{
       // public string AccountNumber { get; set; }
        public string BankId { get; set; }
        public string UserId { get; set; }
        public string Currency { get; set; }

    }
}

