# PowerCMD
PowerCMD is a command execution library that can be used as a base for developer consoles, or even [programming languages](https://github.com/Thev2Andy/PowerExec).

# Features
* Command registry functionality in the `CommandRegistry` class.
```cs
CommandRegistry.RegisterCommand("CMD", new Action<string>(CMDFunc).Method);
CommandRegistry.IsCommandRegistered("CMD");
```

* Command parsing using the `CommandParser` class.
```cs
CommandParser.InitializeParser("`", " ");
CommandParser.ParseCommand("CMD `Hello!`");
```

* Direct command execution using the `CommandExecutionSystem` class.
```cs
CommandExecutionSystem.ExecuteCommand("CMD", "Hello!");
```

* Command invocation using a `Command` instance.
```cs
Command CMDInstance = CommandRegistry.IsCommandRegistered("CMD");
CMDInstance.Execute("Hello!");
```

* Simple, ~~Documented~~ (not yet) API.
* Designed with modularity in mind.
