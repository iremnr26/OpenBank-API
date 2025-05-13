using System;
using Microsoft.AspNetCore.Mvc;
using OpenBankAPI.Infrastructure.Services.Exchange;

namespace OpenBankAPI.API.Controllers
{
        [ApiController]
        [Route("api/exchange")]
        public class ExchangeController : ControllerBase
        {
            private readonly ExchangeRateService _exchangeRateService;

            public ExchangeController(ExchangeRateService exchangeRateService)
            {
                _exchangeRateService = exchangeRateService;
            }

           /* [HttpGet("usd")]
            public async Task<IActionResult> GetUsdRate()
            {
                var result = await _exchangeRateService.GetUsdToTryRate();
                return Ok(result);
            }
           */
        [HttpGet("{currencyCode}")]
        public async Task<IActionResult> GetCurrencyRate(string currencyCode)
        {
            var result = await _exchangeRateService.GetCurrencyRate(currencyCode.ToUpper());
            return Ok(result);
        }

    }

}

