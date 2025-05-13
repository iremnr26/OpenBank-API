using System;
using Domain.Entities;

namespace OpenBankAPI.Application.Abstraction
{
	public interface IBankService
	{
		List<Bank> GetBanks();
		Bank GetBankById(long id);
	}
}

