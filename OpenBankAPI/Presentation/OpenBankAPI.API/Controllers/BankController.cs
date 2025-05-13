using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace OpenBankAPI.API.Controllers
{
    [ApiController]
    [Route("api/banks")]
    public class BankController : ControllerBase
    {
        private readonly IBankWriteRepository _bankWriteRepository;
        private readonly IBankReadRepository _bankReadRepository;

        public BankController(IBankWriteRepository bankWriteRepository, IBankReadRepository bankReadRepository)
        {
            _bankWriteRepository = bankWriteRepository;
            _bankReadRepository = bankReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bankReadRepository.GetAll(false).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _bankReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Bank model)
        {
            await _bankWriteRepository.AddAsync(model);
            await _bankWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Bank model)
        {
            _bankWriteRepository.Update(model);
            await _bankWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _bankWriteRepository.RemoveAsync(id);
            await _bankWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
