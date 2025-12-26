using Zin.Editor.Command;

namespace Zin.Editor.Plugin.Config.API;

public sealed class CommandHandlerAPI
{
    public const string NAME = "CommandHandler";
    private ZinEditor _editor;

    public CommandHandlerAPI(ZinEditor editor)
    {
        _editor = editor;
    }

    public void Bind(string name, int commandId)
    {
        ICommand command = CommandsAPI.GetCommandByInt(commandId);
        if (command is null)
        {
            return;
        }

        _editor.CommandHandler.RegisterCommand(name, command);
    }
}