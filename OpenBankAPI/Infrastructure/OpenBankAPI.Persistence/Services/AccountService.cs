using System;
using Domain.Entities;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Application.Repositories;

namespace OpenBankAPI.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountReadRepository _accountReadRepository;

        public AccountService(IAccountReadRepository accountReadRepository)
        {
            _accountReadRepository = accountReadRepository;
        }

        public List<Account> GetAccounts()
        {
            return _accountReadRepository.GetAll(false).ToList();
        }

        public Account GetAccountById(long id)
        {
            return _accountReadRepository.GetByIdAsync(id.ToString()).Result!;
        }
    }
}

