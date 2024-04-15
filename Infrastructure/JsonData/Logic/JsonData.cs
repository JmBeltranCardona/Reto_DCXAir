using Application.Contracts.JsonData;
using Application.DTOs.Flight;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.JsonData.Logic
{
    public class JsonData : IJsonData
    {
        private readonly string _jsonFilePath;

        public JsonData(IConfiguration configuration)
        {
            _jsonFilePath = configuration["JsonPath"];

        }

        public IEnumerable<FlightDto> GetRoutes()
        {
            string jsonContent = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<FlightDto>>(jsonContent);
        }
    }
}
