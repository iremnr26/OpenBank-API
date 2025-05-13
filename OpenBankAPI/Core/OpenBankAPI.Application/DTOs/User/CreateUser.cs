using System;
namespace OpenBankAPI.Application.DTOs.User
{
	public class CreateUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPass { get; set; }
        public string Username { get; set; }
    }
}

