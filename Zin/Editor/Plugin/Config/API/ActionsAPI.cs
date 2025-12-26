using System;
using Zin.Editor.Input;

namespace Zin.Editor.Plugin.Config.API;

public class ActionsAPI
{
    public const string NAME = "Actions";

    public readonly int Exit = 0;

    public readonly int MoveCursorUp = 1;

    public readonly int MoveCursorDown = 2;

    public readonly int MoveCursorLeft = 3;

    public readonly int MoveCursorRight = 4;

    public readonly int MovePageUp = 5;

    public readonly int MovePageDown = 6;

    public readonly int MoveToStartOfLine = 7;

    public readonly int MoveToEndOfLine = 8;

    public readonly int ChangeToInsertMode = 9;

    public readonly int ChangeToCommandMode = 10;

    public static Action<ZinEditor> GetActionByInt(int i) => i switch
    {
        0 => EditorActions.Exit,
        1 => EditorActions.MoveCursorUp,
        2 => EditorActions.MoveCursorDown,
        3 => EditorActions.MoveCursorLeft,
        4 => EditorActions.MoveCursorRight,
        5 => EditorActions.MovePageUp,
        6 => EditorActions.MovePageDown,
        7 => EditorActions.MoveToStartOfLine,
        8 => EditorActions.MoveToEndOfLine,
        9 => EditorActions.ChangeToInsertMode,
        10 => EditorActions.ChangeToCommandMode,
        _ => null
    };
}