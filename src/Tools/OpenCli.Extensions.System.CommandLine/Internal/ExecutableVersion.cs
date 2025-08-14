using System.Reflection;

namespace System.CommandLine.OpenCli;

// Based on https://github.com/dotnet/command-line-api/blob/main/src/System.CommandLine/RootCommand.cs
// to guarantee similar behavior since RootCommand.ExecutableVersion is internal.
internal static class ExecutableVersion
{
    private static Assembly? _assembly;
    private static string? _executableVersion;

    public static string GetExecutableVersion()
    {
        return _executableVersion ??= GetVersion();

        static string GetVersion()
        {
            _assembly ??= Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var assemblyVersionAttribute = _assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            if (assemblyVersionAttribute is null)
            {
                return _assembly.GetName().Version?.ToString() ?? "1.0";
            }
            else
            {
                return assemblyVersionAttribute.InformationalVersion;
            }
        }
    }
}