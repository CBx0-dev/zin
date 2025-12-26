// Default Key bindings
KeyMap.bind(Keys.ArrowUp, Actions.MoveCursorUp);
KeyMap.bind(Keys.ArrowRight, Actions.MoveCursorRight);
KeyMap.bind(Keys.ArrowDown, Actions.MoveCursorDown);
KeyMap.bind(Keys.ArrowLeft, Actions.MoveCursorLeft);

KeyMap.bind(Keys.PageUp, Actions.MovePageUp);
KeyMap.bind(Keys.PageDown, Actions.MovePageDown);

KeyMap.bind(Keys.Home, Actions.MoveToStartOfLine);
KeyMap.bind(Keys.End, Actions.MoveToEndOfLine);

// Insert Mode specific
KeyMap.bind(Keys.Escape, Actions.ChangeToCommandMode, Modes.InsertMode);

// Command Mode specific
KeyMap.bind(Keys.from("i", false), Actions.ChangeToInsertMode, Modes.CommandMode);

// Commands
CommandHandler.bind(":q", Commands.Quit);
CommandHandler.bind(":o", Commands.Open);