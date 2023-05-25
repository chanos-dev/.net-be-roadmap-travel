### Nuke

#### install nuke tool
```
dotnet tool install Nuke.GlobalTool --global --version 6.3.0
```

#### Nuke setup
```
nuke :setup

// root directory ����
nuke :setup -Root {path}
```

#### Nuke ȣ��
```
nuke {target-name} {paramter}
```

- sample build.cs
```csharp
[Solution(GenerateProjects = true)]
readonly Solution Solution;

[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

Target Foo => _ => _
    .Executes(() =>
    {
        Log.Information("test target");
    });
```

```
nuke Foo --configuration Release
```