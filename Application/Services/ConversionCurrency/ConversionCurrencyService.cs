using Application.Contracts.ConversionCurrency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services.ConversionCurrency
{
    public class ConversionCurrencyService : IConversionCurrencyService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;
        public ConversionCurrencyService(IConfiguration configuration, HttpClient httpClient) 
        {
            _configuration = configuration;
            _httpClient = httpClient;  

        }
        public async Task<double?> ConvertCurrency(string currentCurrency, string currencyToConvert, double amount)
        {
            var baseUrl = _configuration["UrlApiConversion"];
            var apiKey = _configuration["APIKeyConversion"];
            string url = $"{baseUrl}{apiKey}/pair/{currentCurrency}/{currencyToConvert}/{amount}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al realizar la solicitud: {response.StatusCode}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            var jsonResponse = JsonConvert.DeserializeObject<CurrencyConversionResponse>(responseContent);

            return jsonResponse?.conversion_result;
        }

        public class CurrencyConversionResponse
        {
            public double? conversion_result { get; set; }
        }
    }
}
