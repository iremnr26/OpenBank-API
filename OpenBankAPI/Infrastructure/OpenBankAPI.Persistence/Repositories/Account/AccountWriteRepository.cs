using System;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class AccountWriteRepository : WriteRepository<Account>, IAccountWriteRepository
    {
        public AccountWriteRepository(OpenBankAPIDbContext context) : base(context)
        {
        }
    }
}

