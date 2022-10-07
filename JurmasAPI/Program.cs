using JurmasAPI;
using JurmasAPI.Stores;

try
{
    APIConfig.LoadConfig();

    var app = WebApplication.CreateBuilder(args)
        .Initialise()
        .WebApplicationBuilder();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(new string('-', 100));
    Console.WriteLine($"JurmasAPI failed to launch");
    Console.WriteLine(new string('-', 100));
    Console.WriteLine(ex.Message);
    Console.WriteLine(new string('-', 100));
    Console.WriteLine(ex.StackTrace);
    Console.WriteLine(new string('-', 100));

    Environment.Exit(1);
}