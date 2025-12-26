using System.Diagnostics.CodeAnalysis;
using Jint.Runtime;
using Zin.Editor.Plugin.Config.API;

namespace Zin.Editor.Plugin.Config;

public sealed class ConfigHost : Host
{
    private readonly ZinEditor _editor;
    private APIContainer<KeysAPI> _keys;
    private APIContainer<KeyMapAPI> _keyMap;
    private APIContainer<ActionsAPI> _actions;
    private APIContainer<ModesAPI> _modes;
    private APIContainer<CommandsAPI> _commands;
    private APIContainer<CommandHandlerAPI> _commandHandler;

    public ConfigHost(ZinEditor editor)
    {
        _editor = editor;
    }

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(KeysAPI))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(KeyMapAPI))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ActionsAPI))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ModesAPI))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CommandsAPI))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(CommandHandlerAPI))]
    protected override void PostInitialize()
    {
        _keys = new APIContainer<KeysAPI>(Engine, KeysAPI.NAME, new KeysAPI());
        _keyMap = new APIContainer<KeyMapAPI>(Engine, KeyMapAPI.NAME, new KeyMapAPI(_editor));
        _actions = new APIContainer<ActionsAPI>(Engine, ActionsAPI.NAME, new ActionsAPI());
        _modes = new APIContainer<ModesAPI>(Engine, ModesAPI.NAME, new ModesAPI());
        _commands = new APIContainer<CommandsAPI>(Engine, CommandsAPI.NAME, new CommandsAPI());
        _commandHandler = new APIContainer<CommandHandlerAPI>(Engine, CommandHandlerAPI.NAME, new CommandHandlerAPI(_editor));
    }

    public override void InitializeShadowRealm(Realm realm)
    {
        realm.GlobalObject.Set(_keys.Name, _keys.AsJSValue);
        realm.GlobalObject.Set(_keyMap.Name, _keyMap.AsJSValue);
        realm.GlobalObject.Set(_actions.Name, _actions.AsJSValue);
        realm.GlobalObject.Set(_modes.Name, _modes.AsJSValue);
        realm.GlobalObject.Set(_commands.Name, _commands.AsJSValue);
        realm.GlobalObject.Set(_commandHandler.Name, _commandHandler.AsJSValue);
    }
}