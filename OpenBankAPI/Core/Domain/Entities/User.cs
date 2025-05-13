using System;
using System.Security.Principal;
using OpenBankAPI.Domain.Entities.Common;

namespace Domain.Entities
{
	public class User : BaseEntity
    {
        //public string Username { get; set; } // Kullanıcı adı

        public string PasswordHash { get; set; } // Şifre (hashlenmiş)

    }
}

