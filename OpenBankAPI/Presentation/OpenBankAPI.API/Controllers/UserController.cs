using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using OpenBankAPI.Application.Repositories;
using System.Threading.Tasks;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace OpenBankAPI.API.Controllers
{
  /*  [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserWriteRespository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public UserController(IUserWriteRespository userWriteRepository, IUserReadRepository userReadRepository)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userReadRepository.GetAll().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _userReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(User model)
        {
            await _userWriteRepository.AddAsync(model);
            await _userWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(User model)
        {
            _userWriteRepository.Update(model);
            await _userWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userWriteRepository.RemoveAsync(id);
            await _userWriteRepository.SaveAsync();
            return Ok();
        }
    }*/
}
