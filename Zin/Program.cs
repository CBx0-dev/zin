using System.Collections.Generic;
using Zin.Editor;
using Zin.Editor.Plugin.Config;
using Zin.Platform;

namespace Zin;

public static class Program
{
    public static void Main(string[] args)
    {
#if WINDOWS_X64
        using ITerminal terminal = new Platform.Windows.Managed.WindowsTerminal();
#elif LINUX_X64
        using ITerminal terminal = new Platform.Linux.Managed.LinuxTerminal();
#else
#error Unknown target platform
#endif
        terminal.EnableRawMode();

        ZinEditor editor = new ZinEditor(terminal);
        LoadConfig(editor);

        editor.Content.OpenContent("<stdin>", GenerateLines());

        editor.Run();
    }

    private static void LoadConfig(ZinEditor editor)
    {
        using ConfigLoader loader = new ConfigLoader(editor);
        loader.LoadConfig();
    }

    private static IEnumerable<string> GenerateLines()
    {
        for (int i = 0; i < 200; i++)
        {
            yield return  i + "] Hello this is a very long line that must exceeds the windows width. So just in case here are some extra characters that help to achieve this goal 12345";
        }
    }
}