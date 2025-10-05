namespace OpenCli.Extensions.Analyzer.Internal;

public record DiagnosticDescriptor(
    string Id,
    string Title,
    string? Description,
    string? HelpLinkUri,
    string MessageFormat,
    DiagnosticSeverity DefaultSeverity)
{
    public DiagnosticDescriptor(string id, string title, string messageFormat, DiagnosticSeverity defaultSeverity)
        : this(id, title, null, null, messageFormat, defaultSeverity)
    {
    }
}