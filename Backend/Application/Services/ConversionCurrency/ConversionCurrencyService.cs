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
            // Fetch base URL and API key from configuration
            var baseUrl = _configuration["UrlApiConversion"];
            var apiKey = _configuration["APIKeyConversion"];

            // Construct URL for currency conversion API request
            string url = $"{baseUrl}{apiKey}/pair/{currentCurrency}/{currencyToConvert}/{amount}";

            // Send HTTP GET request to currency conversion API
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            // Check if the request was successful
            if (!response.IsSuccessStatusCode)
            {
                // Throw an exception with the error message if request fails
                throw new Exception($"A ocurrido un error durante la conversion, por favor comunicate con el area encargadat: {response.StatusCode}");
            }

            // Read the response content as string
            string responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a CurrencyConversionResponse object
            var jsonResponse = JsonConvert.DeserializeObject<CurrencyConversionResponse>(responseContent);

            // Return the conversion result from the JSON response, if available
            return jsonResponse?.conversion_result;
        }

        public class CurrencyConversionResponse
        {
            public double? conversion_result { get; set; }
        }
    }
}
