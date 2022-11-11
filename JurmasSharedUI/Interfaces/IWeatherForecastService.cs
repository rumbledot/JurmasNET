using JurmasSharedUI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasSharedUI.Interfaces;

public interface IWeatherForecastService
{
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    Task<WeatherForecast[]> GetForecastAsync(int days);
}
