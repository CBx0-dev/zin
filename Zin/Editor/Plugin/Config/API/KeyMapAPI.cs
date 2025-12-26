using System;
using Zin.Editor.Input;

namespace Zin.Editor.Plugin.Config.API;

public sealed class KeyMapAPI
{
    public const string NAME = "KeyMap";
    private ZinEditor _editor;

    public KeyMapAPI(ZinEditor editor)
    {
        _editor = editor;
    }

    public void Bind(KeysAPI.KeyDefinition definition, int actionId) => Bind(definition, actionId, null);

    public void Bind(KeysAPI.KeyDefinition definition, int actionId, ModesAPI.ModeDefinition mode)
    {
        Action<ZinEditor> action = ActionsAPI.GetActionByInt(actionId);
        if (action is null)
        {
            return;
        }

        InputChar inputChar = KeysAPI.KeyDefinition.ToInputChar(definition);
        Shortcut shortcut = new Shortcut(mode?.Type, inputChar);
        _editor.KeyMap.RegisterAction(shortcut, action);
    }

    public void Bind(KeysAPI.KeyDefinition[] definitions, int actionId) => Bind(definitions, actionId, null);

    public void Bind(KeysAPI.KeyDefinition[] definitions, int actionId, ModesAPI.ModeDefinition mode)
    {
        Action<ZinEditor> action = ActionsAPI.GetActionByInt(actionId);
        if (action is null)
        {
            return;
        }

        InputChar[] inputChars = new InputChar[definitions.Length];
        for (int i = 0; i < definitions.Length; i++)
        {
            inputChars[i] = KeysAPI.KeyDefinition.ToInputChar(definitions[i]);
        }

        Shortcut shortcut = new Shortcut(mode?.Type, inputChars);
        _editor.KeyMap.RegisterAction(shortcut, action);
    }
}