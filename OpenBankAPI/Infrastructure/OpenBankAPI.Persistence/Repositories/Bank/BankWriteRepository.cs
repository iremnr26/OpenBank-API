using System;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class BankWriteRepository : WriteRepository<Bank>, IBankWriteRepository
    {
        public BankWriteRepository(OpenBankAPIDbContext context) : base(context)
        {
        }
    }
}

