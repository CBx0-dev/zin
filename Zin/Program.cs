using System;
using Zin.Editor;
using Zin.Editor.Input;
using Zin.Platform.Base;

#if WINDOWS_X64
using Zin.Platform.Windows.Managed;
#elif LINUX_X64
using Zin.Platform.Linux.Managed;
#else
#error Unknown target platform
#endif

namespace Zin;

public static class Program
{
    public static void Main(string[] args)
    {
#if WINDOWS_X64
        using ITerminal terminal = new WindowsTerminal();
#elif LINUX_X64
        using ITerminal terminal = new LinuxTerminal();
#else
#error Unknown target platform
#endif
        KeyMap keyMap = new KeyMap();

        terminal.EnableRawMode();

        ZinEditor editor = new ZinEditor(terminal, keyMap);
        editor.Run();

        // Clean up terminal
        Console.Write("\n\rExiting...\n\r");
    }
}