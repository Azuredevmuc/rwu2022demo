using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace ApiIsolated
{
    public class GetOpenWeatherData
    {
        private readonly ILogger _logger;

        public GetOpenWeatherData(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetOpenWeatherData>();
        }

        static HttpClient client = new HttpClient();

        [Function("GetOpenWeatherData")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            //Get Wether Data of Munich
            string apiKey = Environment.GetEnvironmentVariable("OpenWeatherAPIKey");
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/onecall?lat=48.185777668797314&lon=11.56213033944366&exclude=minutely&appid={apiKey}&units=metric");
            var result = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(result);
        }
    }
}