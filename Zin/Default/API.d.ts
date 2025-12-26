
declare global {
    type char = string;
    type uint8 = number;
    type int32 = number;


    interface KeyDefinition {
        Key: uint8;
        IsCtrl: boolean;
    }

    interface KeysAPI {
        readonly Escape: KeyDefinition;
        readonly ArrowUp: KeyDefinition;
        readonly ArrowDown: KeyDefinition;
        readonly ArrowLeft: KeyDefinition;
        readonly ArrowRight: KeyDefinition;
        readonly PageUp: KeyDefinition;
        readonly PageDown: KeyDefinition;
        readonly Delete: KeyDefinition;
        readonly End: KeyDefinition;
        readonly Home: KeyDefinition;
        readonly Enter: KeyDefinition;
        readonly Backspace: KeyDefinition;

        from(char: char, isCtrl: boolean): KeyDefinition;
    }

    const Keys: KeysAPI;

    interface KeyMapAPI {
        bind(definition: KeyDefinition, action: int32): void;
        bind(definition: KeyDefinition, action: int32, mode: ModeDefinition): void;
        bind(definitions: KeyDefinition[], action: int32): void;
        bind(definitions: KeyDefinition[], action: int32, mode: ModeDefinition): void;
    }

    const KeyMap: KeyMapAPI;

    interface ActionsAPI {
        readonly Exit: int32;
        readonly MoveCursorUp: int32;
        readonly MoveCursorDown: int32;
        readonly MoveCursorLeft: int32;
        readonly MoveCursorRight: int32;
        readonly MovePageUp: int32;
        readonly MovePageDown: int32;
        readonly MoveToStartOfLine: int32;
        readonly MoveToEndOfLine: int32;
        readonly ChangeToInsertMode: int32;
        readonly ChangeToCommandMode: int32;
    }

    const Actions: ActionsAPI;

    interface ModeDefinition {
        readonly accessName: string;
        readonly type: never;
    }

    interface ModesAPI {
        readonly InsertMode: ModeDefinition;
        readonly CommandMode: ModeDefinition;

        getMode(name: string): ModeDefinition | null;
    }

    const Modes: ModesAPI;

    interface CommandsAPI {
        readonly Quit: int32;
        readonly Open: int32;
    }

    const Commands: CommandsAPI;

    interface CommandHandlerAPI {
        bind(name: string, command: int32): void;
    }

    const CommandHandler: CommandHandlerAPI;
}

export {}