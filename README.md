# PowerCMD
PowerCMD is a command execution library that can be used as a base for developer consoles, or even [programming languages](https://github.com/Thev2Andy/PowerExec).

# Features
* Command registry functionality in the `CommandRegistry` class.
```cs
Registry.RegisterCommand("CMD", new Action<string>(CMDFunc).Method);
Registry.IsCommandRegistered("CMD");
```

* Command parsing using the `CommandParser` class.
```cs
Parser.InitializeParser("`", " ");
Parser.ParseCommand("CMD `Hello!`", ExecutionSystem);
```

* Direct command execution using the `CommandExecutionSystem` class.
```cs
ExecutionSystem.ExecuteCommand("CMD", "Hello!");
```

* Command invocation using a `Command` instance.
```cs
Command CMDInstance = Registry.IsCommandRegistered("CMD");
CMDInstance.Execute("Hello!");
```

* Simple, ~~Documented~~ (not yet) API.
* Designed with modularity in mind.
