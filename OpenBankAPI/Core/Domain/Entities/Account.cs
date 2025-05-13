using System;
using System.Collections.Generic;
using System.Xml;
using OpenBankAPI.Domain.Entities.Common;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {

        public int UserId { get; set; } // Foreign Key: User tablosuna referans

        public Guid UserGuidId { get; set; }

        public string BankId { get; set; }// banka idsi

        public string AccountNumber { get; set; } // Hesap numarası

        public decimal Balance { get; set; } // Hesap bakiyesi

        public string Currency { get; set; } // Para birimi (örneğin USD, EUR)


        public Account()
        {
            AccountNumber = GenerateAccountNumber();
        }

        public string GenerateAccountNumber()
        {
            Random random = new Random();
            return $"{random.Next(10000000, 99999999)}{random.Next(10000000, 99999999)}";
        }
    }

   
}

