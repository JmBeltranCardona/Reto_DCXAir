using Domain.DTOs;
using Infrastructure.JsonData.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.JsonData.Logic
{
    public class JsonData : IJsonData
    {
        private string? _jsonFilePath;

        public JsonData(IConfiguration configuration)
        {
            _jsonFilePath = configuration["JsonPath"];

        }

        public IEnumerable<FlightDTO> GetRoutes()
        {
            string jsonContent = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<FlightDTO>>(jsonContent);
        }
    }
}
