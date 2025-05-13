 using System;
using Domain.Entities;

namespace OpenBankAPI.Application.Abstraction
{
	public interface IProductionService
	{
		List<Product> GetProducts(); 

	}
}

