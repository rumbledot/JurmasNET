using System.Runtime.InteropServices;
using System.Text;

namespace JurmasAPI.Helpers;

public class ConfigParser : IDisposable
{
    public string config_path;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section,
             string key, string def, StringBuilder retVal,
        int size, string filePath);

    public ConfigParser(string config_path)
    {
        if (!File.Exists(config_path))
        {
            throw new IOException("File not found");
        }

        this.config_path = config_path;
    }

    public void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, this.config_path);
    }

    public string IniReadValue(string Section, string Key, int size = 255)
    {
        StringBuilder temp = new StringBuilder(size);
        int i = GetPrivateProfileString(Section, Key, "", temp, size, this.config_path);

        return temp.ToString();
    }

    #region IDisposable Implementation
    private bool disposed = false;

    ~ConfigParser()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {

            }
            else
            {

            }

            disposed = true;
        }
    }
    #endregion
}
