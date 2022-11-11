using JurmasSharedUI.Interfaces;
using JurmasSharedUI.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace JurmasMAUI.Data
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private HttpClient Http;

        public WeatherForecastService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }

        public async Task<WeatherForecast[]> GetForecastAsync(int days)
        {
            return await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
        }
    }
}