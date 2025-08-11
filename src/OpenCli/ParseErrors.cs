namespace OpenCli;

internal sealed class ParseErrors
{
    public static DiagnosticDescriptor SchemaError(string message) =>
        new DiagnosticDescriptor("OPENCLI-0001", Severity.Error, "Schema validation error", message);

    public static DiagnosticDescriptor InvalidJson() =>
        new DiagnosticDescriptor("OPENCLI-0002", Severity.Error, "Invalid JSON", "The provided JSON was invalid");
}