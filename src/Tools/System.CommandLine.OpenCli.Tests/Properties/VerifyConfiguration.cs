using System.Runtime.CompilerServices;

namespace System.CommandLine.OpenCli.Tests;

public static class VerifyConfiguration
{
    [ModuleInitializer]
    public static void Init()
    {
        Verifier.DerivePathInfo(Expectations.Initialize);
    }
}