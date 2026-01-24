using Luqma.Data.Helpers;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json; 
using System.Text.Json;
namespace Luqma.Service.Implementations
{
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenWeatherSettings _openWeatherSettings;

        public WeatherService(HttpClient httpClient,OpenWeatherSettings openWeatherSettings)
        {
            _httpClient = httpClient;
            _openWeatherSettings = openWeatherSettings;
        }
        public async Task<string> GetWeatherConditionAsync(string city)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city},PS&appid={_openWeatherSettings.ApiKey}&units=metric";
                var response = await _httpClient.GetFromJsonAsync<JsonElement>(url);

                if (response.TryGetProperty("weather", out JsonElement weatherArray) && weatherArray.GetArrayLength() > 0)
                {
                    var mainWeather = weatherArray[0].GetProperty("main").GetString();
                    return mainWeather ?? "Unknown";
                }

                return "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}

