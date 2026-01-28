using System;
using Zin.Editor.Input;

namespace Zin.Platform.Linux.Managed;

public sealed class LinuxTerminal : ITerminal
{
    public int Width => Console.WindowWidth;
    public int Height => Console.WindowHeight;

    public LinuxTerminal()
    {
    }

    public void EnableRawMode()
    {
        if (TermShim.EnterRawMode() != 1)
        {
            throw new Exception("Fail to enable raw mode");
        }
    }

    public void DisableRawMode()
    {
        if (TermShim.ExitRawMode() != 1)
        {
            throw new Exception("Fail to disable raw mode");
        }
    }

    public InputChar Read()
    {
        if (TermShim.Read(out short c) != 1)
        {
            return new InputChar(0);
        }

        if (c < 256)
        {
            return new InputChar((byte)c);
        }

        return new InputChar((InputChar.EscapeCode)(byte)(c - 256), true);
    }

    public void Write(string text) => TermShim.Write(text, text.Length);

    public void Dispose()
    {
        DisableRawMode();
    }
}