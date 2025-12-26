using Zin.Editor.Command;

namespace Zin.Editor.Plugin.Config.API;

public sealed class CommandsAPI
{
    public const string NAME = "Commands";

    public readonly int Quit = 0;
    public readonly int Open = 1;

    public static ICommand GetCommandByInt(int i) => i switch
    {
        0 => new QuitCommand(),
        1 => new OpenCommand(),
        _ => null
    };
}