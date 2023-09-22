# ASP.Net Configuration Binding Source Generator repro

This repository reproduces an issue in .Net 8 RC1 demonstrating the source-generated ASP.Net Configuration binder generates uncompilable code.

OS: Windows 10 22H2
Dotnet SDK version: 8.0.100-rc.1.23463.5

With default usage, the generator produces the following errors:
* Missing usings for `Microsoft.Extensions.Options`
  * Easily worked around by adding `Microsoft.Extensions.Options` to implicit global usings in the csproj.
  * ```
    <ItemGroup>
      <Using Include="Microsoft.Extensions.Options" />
    </ItemGroup>
    ```
* Invalid source produced. See [BindingExtensions.g.cs](./ConfigBinderGeneratorRepro/Generated/net8.0/Microsoft.Extensions.Configuration.Binder.SourceGeneration/Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator/BindingExtensions.g.cs) line 57.
  * ```
    // Missing comma near the end of the line 👉 ----------------------------------------------------------------------------------------------------------------------------------------------------------------------> 👇
    return services.AddSingleton<Microsoft.Extensions.Options.IConfigureOptions<TOptions>>(new Microsoft.Extensions.Options.ConfigureNamedOptions<TOptions>(name, obj => BindCoreMain(configuration, obj, typeof(TOptions)configureOptions)));
    ```

# Build Errors

```
> dotnet --list-sdks
**snip various pre-8 SDK versions**
8.0.100-rc.1.23463.5 [C:\Program Files\dotnet\sdk]

> dotnet build
MSBuild version 17.8.0-preview-23418-03+0125fc9fb for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
C:\Program Files\dotnet\sdk\8.0.100-rc.1.23463.5\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(311,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-suppor
t-policy [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro.csproj]
C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs(
57,227): error CS1003: Syntax error, ',' expected [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro.csproj]
C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs( 
56,35): error CS0246: The type or namespace name 'IOptionsChangeTokenSource<>' could not be found (are you missing a using directive or an assembly reference?) [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\ConfigBind 
erGeneratorRepro.csproj]
C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs( 
57,227): error CS1003: Syntax error, ',' expected [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro.csproj]
C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs( 
56,35): error CS0246: The type or namespace name 'IOptionsChangeTokenSource<>' could not be found (are you missing a using directive or an assembly reference?) [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\ConfigBind 
erGeneratorRepro.csproj]
C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs( 
56,76): error CS0246: The type or namespace name 'ConfigurationChangeTokenSource<>' could not be found (are you missing a using directive or an assembly reference?) [C:\pd\ConfigBinderGeneratorRepro\ConfigBinderGeneratorRepro\Confi 
gBinderGeneratorRepro.csproj]
    0 Warning(s)
    3 Error(s)

Time Elapsed 00:00:01.21
```
