using System;
using Microsoft.Extensions.Configuration;

namespace OpenBankAPI.Infrastructure.Services.Exchange
{
	public class ExchangeRateService
	{
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

      /*  public async Task<string> GetUsdToTryRate()
        {
            var apiKey = _configuration["EVDS:ApiKey"];
            string url = "https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.A&startDate=01-04-2024&endDate=10-04-2024&type=json";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("key", apiKey); // ✅ KEY BURAYA!

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }*/

        // 🔄 Genel method, tüm currencyCode'lar için çalışır
        public async Task<string> GetCurrencyRate(string currencyCode)
        {
            var apiKey = _configuration["EVDS:ApiKey"];
            var startDate = "01-04-2024";
            var endDate = DateTime.Today.ToString("dd-MM-yyyy");

            string url = $"https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.{currencyCode}.A&startDate={startDate}&endDate={endDate}&type=json";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("key", apiKey); // 🔐 API Key'i header olarak gönder

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"EVDS API error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

            return await response.Content.ReadAsStringAsync();
        }


    }
}

