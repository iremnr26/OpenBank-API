using System;
using OpenBankAPI.Domain.Entities.Common;

namespace Domain.Entities
{
	public class Bank: BaseEntity
    {
        public string BankName { get; set; } // Banka adı

        public string BankCode { get; set; } // Banka kodu (örneğin SWIFT kodu)        

    }
}

