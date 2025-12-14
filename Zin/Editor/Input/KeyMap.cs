using Zin.Platform.Base;

namespace Zin.Editor.Input;

public sealed class KeyMap
{
    public InputChar ExitShortCut = new InputChar('q', true);

    public KeyMap()
    {

    }

    public void RunKeyShortcut(ZinEditor editor, InputChar input)
    {
        if (input == ExitShortCut)
        {
            editor.Stop();
            return;
        }
    }
}