using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAppp.Models;

namespace WeatherAppp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string API_KEY = "e1f9d6724d88fae11fdca0efb8c54c26";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        // method to get weather by coordinates
        public async Task<WeatherModel.WeatherResponse> GetWeatherByCoordinates(double latitude, double longitude)
        {
           // latitude = -23.883669;
            //longitude = 29.707878;
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric");

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<WeatherModel.WeatherResponse>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Failed to get weather data");
        }
        // grtting weather by city
        public async Task<WeatherModel.WeatherResponse> GetWeatherByCity(string city)
        {
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}&units=metric");

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<WeatherModel.WeatherResponse>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            throw new Exception("Failed to get weather data");
        }
    }
}
