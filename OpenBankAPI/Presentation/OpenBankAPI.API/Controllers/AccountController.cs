using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using OpenBankAPI.Application.Abstraction;
using OpenBankAPI.Application.Repositories;
using OpenBankAPI.Application.ViewModels.Accounts;
using System.Net;
using Microsoft.EntityFrameworkCore;
using OpenBankAPI.Persistence.Repositories;
using MediatR;
using OpenBankAPI.Application.Features.Queries.GetAllAccounts;
using Microsoft.AspNetCore.Authorization;

namespace OpenBankAPI.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        readonly private IAccountWriteRepository _accountWriteRepository;
        private readonly IAccountReadRepository _accoutReadRepository;
        readonly IMediator _mediator;
        readonly ILogger<AccountController> _logger;
        readonly IAccountService _accountService;

        public AccountController(
            IMediator mediator,
            ILogger<AccountController> logger,
            IAccountService accountService,
            IAccountWriteRepository accountWriteRepository,
            IAccountReadRepository accountReadRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _accountService = accountService;
            _accountWriteRepository = accountWriteRepository;
            _accoutReadRepository = accountReadRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllAccountsQueryRequest getAllAccountsQueryRequest)
        {
            _logger.LogInformation("✅ Authorize attribute çalıştı!");
         
            GetAllAccountsQueryResponse response = await _mediator.Send(getAllAccountsQueryRequest);
            return Ok(response);
        }

       /* [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var query = _accoutReadRepository.GetAll(false);

            var totalCount = await query.CountAsync(); // Toplam veri sayısı
            var accounts = await query
                .OrderBy(a => a.Id) // Id’ye göre sıralama
                .Skip((page - 1) * pageSize) // Sayfa atlama
                .Take(pageSize) // Sayfa boyutuna göre veri alma
                .Select(p => new
                {
                    p.Id,
                    p.UserId,
                    p.AccountNumber,
                    p.BankId,
                    p.Balance,
                    p.Currency
                })
                .ToListAsync();

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                data = accounts
            });
        }*/



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _accoutReadRepository.GetByIdAsync(id,false));
        }
      

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Account model)
        {
            if(ModelState.IsValid){

            }
            {
                var newAccount = new Account()
                {
                    BankId = model.BankId,
                    UserId = int.Parse(model.UserId),
                    Currency = model.Currency,
                    Balance = 0,  // Varsayılan olarak 0
                    CreatedBy = model.UserId,
                    UpdatedBy = model.UserId,
                    AccountNumber = new Account().GenerateAccountNumber(), // 🔥 AccountNumber burada oluşturuluyor!
                    UniqueId = Guid.NewGuid() // 🔥 Burada UniqueId oluşturuluyor!

                };

                await _accountWriteRepository.AddAsync(newAccount);
                await _accountWriteRepository.SaveAsync(); // Veri kaydediliyor
                return Ok(new { message = "Account created successfully", accountNumber = newAccount.AccountNumber });

            }
        }

        [HttpPut]
        public async Task<IActionResult> Put( VM_Update_Account model)
        { 
            Account account = await _accoutReadRepository.GetByIdAsync(model.Id);
            account.Balance = model.Balance;
            account.Currency = model.Currency;
           await _accountWriteRepository.SaveAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // ID'nin int olduğundan emin ol!
        {
            var account = await _accoutReadRepository.GetByIdAsync(id.ToString());
            if (account == null)
            {
                Console.WriteLine($"❌ ERROR: ID bulunamadı: {id}");
                return NotFound($"Account with ID {id} not found.");
            }

            await _accountWriteRepository.RemoveAsync(id.ToString());
            await _accountWriteRepository.SaveAsync();

            Console.WriteLine($"✅ Silme işlemi tamamlandı, ID: {id}");
            return Ok(new { message = "Silme başarılı" }); // BU SATIR ÖNEMLİ
        }




    }

}

