using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;
using MediatR;
using OpenBankAPI.Application.Features.Commands.CreateTransaction;
using Microsoft.AspNetCore.Authorization;
using OpenBankAPI.Application.Features.Queries.GetAllTransactions;
using OpenBankAPI.Persistence.Repositories;

namespace OpenBankAPI.API.Controllers
{

    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITransactionReadRepository _transactionReadRepository;
        private readonly ITransactionWriteRepository _transactionWriteRepository;

        public TransactionController(IMediator mediator,ITransactionReadRepository transactionReadRepository,ITransactionWriteRepository transactionWriteRepository)
        {
            _mediator = mediator;
            _transactionReadRepository = transactionReadRepository;
            _transactionWriteRepository = transactionWriteRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionCommand command)
        {

            var response = await _mediator.Send(command);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] GetAllTransactionsQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // ID'nin int olduğundan emin ol!
        {
            var account = await _transactionReadRepository.GetByIdAsync(id.ToString());
            if (account == null)
            {
                Console.WriteLine($"❌ ERROR: ID bulunamadı: {id}");
                return NotFound($"Account with ID {id} not found.");
            }

            await _transactionWriteRepository.RemoveAsync(id.ToString());
            await _transactionWriteRepository.SaveAsync();

            Console.WriteLine($"✅ Silme işlemi tamamlandı, ID: {id}");
            return Ok(new { message = "Silme başarılı" }); // BU SATIR ÖNEMLİ
        }


    }

    /*[ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionWriteRepository _transactionWriteRepository;
        private readonly ITransactionReadRepository _transactionReadRepository;

        public TransactionController(ITransactionWriteRepository transactionWriteRepository, ITransactionReadRepository transactionReadRepository)
        {
            _transactionWriteRepository = transactionWriteRepository;
            _transactionReadRepository = transactionReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _transactionReadRepository.GetAll(false).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _transactionReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Transaction model)
        {
            await _transactionWriteRepository.AddAsync(model);
            await _transactionWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Transaction model)
        {
            _transactionWriteRepository.Update(model);
            await _transactionWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _transactionWriteRepository.RemoveAsync(id);
            await _transactionWriteRepository.SaveAsync();
            return Ok();
        }
    }*/
}
