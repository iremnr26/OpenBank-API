using System;
namespace OpenBankAPI.Application.DTOs
{
	public class Token
	{
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}

