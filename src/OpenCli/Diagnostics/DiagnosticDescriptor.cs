namespace OpenCli;

[PublicAPI]
public sealed class DiagnosticDescriptor
{
    public string? Code { get; }
    public string Summary { get; }
    public string Message { get; }
    public Severity Severity { get; }

    public DiagnosticDescriptor(string? code, Severity severity, string summary, string message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Severity = severity;
        Summary = summary;
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public Diagnostic ToDiagnostic(Location? span)
    {
        return new Diagnostic(Severity, Summary, Message) { Code = Code, Location = span, };
    }
}