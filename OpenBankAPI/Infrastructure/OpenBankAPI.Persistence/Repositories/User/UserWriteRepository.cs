 using System;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Persistence.Contexts;

namespace OpenBankAPI.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRespository

    {
        public UserWriteRepository(OpenBankAPIDbContext context) : base(context)
        {
        }
    }
}

