using JurmasMAUI.Data;
using JurmasSharedUI.Interfaces;
using JurmasWEB;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:8888") });

builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();

await builder.Build().RunAsync();
