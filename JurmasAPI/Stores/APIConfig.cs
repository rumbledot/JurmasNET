using JurmasAPI.Helpers;

namespace JurmasAPI.Stores;

public static class APIConfig
{
    //Event logging
    public const string LOG_SOURCE = "Jurmas_Log_Source";
    public const string LOG_SOURCE_NAME = "Jurmas_Log";

    //JWT Token builder components
    public const string JWT_ISSUER = "rumbledot";
    public const string JWT_KEY = "Jurmas, Jurnal masak pintar";

    public static string SETTINGS_LOCATION { get; set; } = string.Empty;
    public static string LOG_LOCATION { get; set; } = string.Empty;
    public static int HOST_PORT { get; set; } = 8000;
    public static int API_TIMEOUT { get; set; } = 3000000;

    public static void LoadConfig()
    {
        try
        {
            SETTINGS_LOCATION = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api-config.ini");

            if (!File.Exists(SETTINGS_LOCATION))
            {
                Console.WriteLine("api-config.ini file not found");
                throw new Exception();
            }

            Console.WriteLine($"reading api-config.ini file in {SETTINGS_LOCATION}");

            ConfigParser config = new ConfigParser(SETTINGS_LOCATION);

            //fallback to 8000
            if (int.TryParse(config.IniReadValue("ServerSettings", "host_port"), out int port))
            {
                HOST_PORT = port;
            }

            //fallback to 30000000 (3 secs)
            if (int.TryParse(config.IniReadValue("ServerSettings", "api_timeout"), out int timout))
            {
                API_TIMEOUT = timout * 10000000; //convert sec to milisec
            }

            LOG_LOCATION = config.IniReadValue("ServerSettings", "log_location", 400);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Config file read failed\n{ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
}