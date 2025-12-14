using System.Reflection;
using Zin.Editor.Input;
using Zin.Platform.Base;

namespace Zin.Editor;

public sealed class ZinEditor
{
    private static string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    private static string Title => "Zin Editor";

    private readonly ITerminal _terminal;
    private readonly KeyMap _keyMap;
    private bool _stopped;
    private RenderChain _renderChain;
    private Cursor _cursor;

    public ZinEditor(ITerminal terminal, KeyMap keyMap)
    {
        _terminal = terminal;
        _keyMap = keyMap;
        _stopped = false;
        _renderChain = new RenderChain(terminal.Width, terminal.Height);
        _cursor = new Cursor();
    }

    public void Run()
    {
        Render();

        while (true)
        {
            InputChar c = _terminal.Read();
            if (c.Invalid)
            {
                continue;
            }

            Render();

            // Console.Write($"{c.Raw} (Ctrl: {c.Ctrl}) (Char: {c.Char})\n\r");
            _keyMap.RunKeyShortcut(this, c);

            if (_stopped)
            {
                _renderChain.PrepareRender();
                _renderChain.ClearScreen();
                _renderChain.MoveCursor();
                _terminal.Write(_renderChain.Render());
                break;
            }
        }
    }

    public void Stop() => _stopped = true;

    private void Render()
    {
        _renderChain.PrepareRender();
        _renderChain.HideCursor();

        RenderRows();

        string title = $"{Title} v{Version}";
        _renderChain.MoveCursor(_cursor.X + 3, _cursor.Y + 1);
        _renderChain.Write(title);

        _renderChain.MoveCursor();
        _renderChain.ShowCursor();
        _terminal.Write(_renderChain.Render());
    }

    private void RenderRows()
    {
        for (int y = 0; y < _terminal.Height - 1; y++)
        {
            _renderChain.Write('~');
            _renderChain.ClearLine();
            _renderChain.LineBreak();
        }

        _renderChain.Write("~");
        _renderChain.MoveCursor();
    }
}