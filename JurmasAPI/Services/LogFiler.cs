using JurmasAPI.Stores;

namespace JurmasAPI.Services;

public interface ILogFiler
{
    void Initialise(string folderLocation);
    void Log(string message, string logAs);
}

public class LogFiler : ILogFiler
{
    private bool initialised = false;
    private string folderLocation = string.Empty;

    public LogFiler()
    {
        try
        {
            this.folderLocation = APIConfig.LOG_LOCATION;

            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            this.initialised = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Log filer exception\n{ex.Message}\n{ex.StackTrace}");

            this.initialised = false;
        }
    }

    public void Initialise(string folderLocation)
    {
        if (this.initialised) return;

        try
        {
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }

            this.folderLocation = folderLocation;
            this.initialised = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Log filer exception\n{ex.Message}\n{ex.StackTrace}");

            this.initialised = false;
        }
    }

    public void Log(string message, string logAs = "Log")
    {
        if (!this.initialised) return;

        File.WriteAllText(Path.Combine(this.folderLocation, $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm")}_{logAs}.txt"), message);
    }
}
