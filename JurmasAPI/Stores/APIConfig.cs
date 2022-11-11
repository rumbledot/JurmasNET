namespace JurmasAPI.Stores;

public static class APIConfig
{
    //Event logging
    public const string LOG_SOURCE = "Jurmas_Log_Source";
    public const string LOG_SOURCE_NAME = "Jurmas_Log";

    public static int HOST_PORT { get; set; } = 8000;
    public static int API_TIMEOUT { get; set; } = 3000000;
    public static string DBCONN { get; set; } = string.Empty;

    //JWT Token builder components
    public static string JWT_ISSUER = "rumbledot";
    public static string JWT_AUDIENCE = "rumbledot";
    public static string JWT_KEY = "Jurmas, Jurnal masak pintar";

    public static string LOG_LOCATION = string.Empty;

    public static void LoadConfig()
    {
        try
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

            //fallback to 8000
            if (int.TryParse(config.GetSection("ServerSettings")["host_port"], out int port))
            {
                HOST_PORT = port;
            }

            //fallback to 30000000 (3 secs)
            if (int.TryParse(config.GetSection("ServerSettings")["api_timeout"], out int timout))
            {
                API_TIMEOUT = timout * 10000000; //convert sec to milisec
            }

            DBCONN = config.GetConnectionString("Default");

            JWT_ISSUER = config.GetSection("JWT")["JWT_ISSUER"].ToString();
            JWT_AUDIENCE = config.GetSection("JWT")["JWT_AUDIENCE"].ToString();
            JWT_KEY = config.GetSection("JWT")["JWT_KEY"].ToString();

            LOG_LOCATION = AppDomain.CurrentDomain.BaseDirectory;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Config file read failed\n{ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
}