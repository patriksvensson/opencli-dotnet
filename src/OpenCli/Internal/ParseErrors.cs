#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using OpenCli.Diagnostics;

namespace OpenCli.Internal;

internal sealed class ParseErrors
{
    public static OpenCliDiagnosticDescriptor InvalidJson() =>
        new OpenCliDiagnosticDescriptor("OPENCLI-0001", OpenCliSeverity.Error, "Invalid JSON", "The provided JSON was invalid");

    public static OpenCliDiagnosticDescriptor SchemaError(string message) =>
        new OpenCliDiagnosticDescriptor("OPENCLI-0002", OpenCliSeverity.Error, "Schema validation error", message);
}