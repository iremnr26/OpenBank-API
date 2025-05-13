﻿using System;
namespace OpenBankAPI.Application.Exceptions
{
	public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("User Name or Passsword is wrong!")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

