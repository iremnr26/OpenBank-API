using System;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class AccountReadRepository : ReadRepository<Account>, IAccountReadRepository
    {
        public AccountReadRepository(OpenBankAPIDbContext context) : base(context)
        {
        }
    }
}

