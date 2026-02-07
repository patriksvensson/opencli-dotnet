namespace OpenCli.Extensions.Analyzer.Internal;

public record Diagnostic(string Id, DiagnosticSeverity Severity, string Message)
{
    // TODO: support location
    public static Diagnostic Create(DiagnosticDescriptor description, params object[] args)
    {
        return new Diagnostic(description.Id, description.DefaultSeverity, string.Format(description.MessageFormat, args));
    }
}