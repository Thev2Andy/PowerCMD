# PowerCMD
PowerCMD is a command execution library that can be used as a base for developer consoles, or even [programming languages](https://github.com/Thev2Andy/PowerExec).

# Features
* Command registry functionality in the `Registry` class.
```cs
Registry.Register("CMD", new Action<string>(CMDFunc).Method);
Registry.IsRegistered("CMD");
```

* Command parsing using the `Parser` class.
```cs
Parser.Parse("CMD `Hello!`", ExecutionSystem);
```

* Direct command execution using the `CommandExecutionSystem` class.
```cs
ExecutionSystem.Execute("CMD", "Hello!");
```

* Command invocation using a `Command` instance.
```cs
Command CMDInstance = Registry.IsRegistered("CMD");
CMDInstance.Execute("Hello!");
```

* Simple, ~~Documented~~ (not yet) API.
* Designed with modularity in mind.
