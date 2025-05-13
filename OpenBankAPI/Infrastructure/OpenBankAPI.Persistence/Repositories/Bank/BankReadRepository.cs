using System;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class BankReadRepository : ReadRepository<Bank>, IBankReadRepository
    {
        public BankReadRepository(OpenBankAPIDbContext context) : base(context)
        {
        }
    }
}

