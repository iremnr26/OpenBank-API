﻿using System;
namespace OpenBankAPI.Application.Exceptions
{
	public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("authentication error!")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

