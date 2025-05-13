using System;
using Domain.Entities;

namespace OpenBankAPI.Application.Abstraction
{
	public interface ITransactionService
	{
        List<Transaction> GetTransactions(); // Tüm işlemleri getir
        Transaction GetTransactionById(long id);


    }
}

