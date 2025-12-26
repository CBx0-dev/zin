using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Zin.Editor.Mode;

namespace Zin.Editor.Plugin.Config.API;

public sealed class ModesAPI
{
    public static List<ModeDefinition> Modes = new List<ModeDefinition>(2);
    public const string NAME = "Modes";

    public readonly ModeDefinition InsertMode;
    public readonly ModeDefinition CommandMode;

    public ModesAPI()
    {
        InsertMode = ModeDefinition.Create<InsertMode>("Core.Insert");
        Modes.Add(InsertMode);

        CommandMode = ModeDefinition.Create<CommandMode>("Core.Command");
        Modes.Add(CommandMode);
    }

    public static ModeDefinition GetMode(string accessName)
    {
        foreach (ModeDefinition mode in Modes)
        {
            if (mode.AccessName == accessName)
            {
                return mode;
            }
        }

        return null;
    }

    public class ModeDefinition
    {
        public readonly string AccessName;
        internal readonly Type Type;

        private ModeDefinition(string accessName, Type type)
        {
            AccessName = accessName;
            Type = type;
        }

        public static ModeDefinition Create<T>(string accessName) where T : EditorMode =>
            new ModeDefinition(accessName, typeof(T));
    }
}