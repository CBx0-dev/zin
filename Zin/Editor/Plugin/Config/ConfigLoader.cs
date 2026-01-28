using System;
using System.IO;
using System.Reflection;
using Jint;
using Jint.Native.ShadowRealm;

namespace Zin.Editor.Plugin.Config;

public class ConfigLoader : IDisposable
{
    private const string EMBEDDED_DEFAULT_CONFIG = "Zin.Default.Default.js";
    private const string CONFIG_DEFAULT_FILE_NAME = "default.js";
    private const string CONFIG_FILE_NAME = "config.js";

#if WINDOWS_X64
    private const string LOCAL_CONFIG_FOLDER_NAME = "zin";
    private const string GLOBAL_CONFIG_FOLDER_NAME = "zin";
#elif LINUX_X64
    private const string LOCAL_CONFIG_FOLDER_NAME = ".zin";
    private const string GLOBAL_CONFIG_FOLDER_NAME = "zin";
#else
#error Unknown target platform
#endif

    public readonly static string GlobalConfigPath;
    public readonly static string LocalConfigPath;

    private Engine _engine;

    public ConfigLoader(ZinEditor editor)
    {
        _engine = new Engine((engine, options) =>
        {
            options.Strict = true;
            options.Debugger.Enabled = false;
            options.UseHostFactory(engine => new ConfigHost(editor));
        });
    }

    public void LoadConfig()
    {
        string defaultConfigPath = Path.Combine(GlobalConfigPath, CONFIG_DEFAULT_FILE_NAME);
        if (TryLoadConfigFile(defaultConfigPath) == 1)
        {
            Directory.CreateDirectory(GlobalConfigPath);
            
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EMBEDDED_DEFAULT_CONFIG);
            if (stream is null)
            {
                throw new FileNotFoundException(EMBEDDED_DEFAULT_CONFIG);
            }

            using FileStream file = File.OpenWrite(defaultConfigPath);
            stream.CopyTo(file);
            file.Close();
            stream.Close();

            TryLoadConfigFile(defaultConfigPath);
        }
        TryLoadConfigFile(Path.Combine(GlobalConfigPath, CONFIG_FILE_NAME));
        TryLoadConfigFile(Path.Combine(LocalConfigPath, CONFIG_FILE_NAME));
    }

    private byte TryLoadConfigFile(string file)
    {
        if (!File.Exists(file))
        {
            return 1;
        }

        string content = File.ReadAllText(file);
        ShadowRealm realm = _engine.Intrinsics.ShadowRealm.Construct();
        try
        {
            realm.Evaluate(content);
        }
        catch
        {
            return 2;
        }

        return 0;
    }

    public void Dispose()
    {
        _engine.Dispose();
    }

    static ConfigLoader()
    {
#if WINDOWS_X64
        GlobalConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), GLOBAL_CONFIG_FOLDER_NAME);
        LocalConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), LOCAL_CONFIG_FOLDER_NAME);
#elif LINUX_X64
        GlobalConfigPath = Path.Combine("/etc", GLOBAL_CONFIG_FOLDER_NAME);
        LocalConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), LOCAL_CONFIG_FOLDER_NAME);
#else
#error Unknown target platform
#endif
    }
}