using System.Diagnostics.CodeAnalysis;
using Zin.Editor.Input;

namespace Zin.Editor.Plugin.Config.API;

public sealed class KeysAPI
{
    public const string NAME = "Keys";

    public readonly KeyDefinition Escape = new KeyDefinition(InputChar.EscapeCode.Escape);

    public readonly KeyDefinition ArrowUp = new KeyDefinition(InputChar.EscapeCode.ArrowUp);

    public readonly KeyDefinition ArrowDown = new KeyDefinition(InputChar.EscapeCode.ArrowDown);

    public readonly KeyDefinition ArrowLeft = new KeyDefinition(InputChar.EscapeCode.ArrowLeft);

    public readonly KeyDefinition ArrowRight = new KeyDefinition(InputChar.EscapeCode.ArrowRight);

    public readonly KeyDefinition PageUp = new KeyDefinition(InputChar.EscapeCode.PageUp);

    public readonly KeyDefinition PageDown = new KeyDefinition(InputChar.EscapeCode.PageDown);

    public readonly KeyDefinition Delete = new KeyDefinition(InputChar.EscapeCode.Delete);

    public readonly KeyDefinition End = new KeyDefinition(InputChar.EscapeCode.End);

    public readonly KeyDefinition Home = new KeyDefinition(InputChar.EscapeCode.Home);

    public readonly KeyDefinition Enter = new KeyDefinition(InputChar.EscapeCode.Enter);

    public readonly KeyDefinition Backspace = new KeyDefinition(InputChar.EscapeCode.Backspace);

    public KeyDefinition From(string key, bool isCtrl) => new KeyDefinition((byte)key[0], isCtrl);

    public class KeyDefinition
    {
        public byte Key;
        public bool IsCtrl;
        public bool Escaped;

        public KeyDefinition(byte key, bool isCtrl)
        {
            Key = key;
            IsCtrl = isCtrl;
            Escaped = false;
        }

        public KeyDefinition(InputChar.EscapeCode key)
        {
            Key = (byte)key;
            IsCtrl = false;
            Escaped = true;
        }

        public static InputChar ToInputChar(KeyDefinition definition) => definition.Escaped
            ? new InputChar((InputChar.EscapeCode)definition.Key, true)
            : new InputChar((char)definition.Key, definition.IsCtrl);
    }
}