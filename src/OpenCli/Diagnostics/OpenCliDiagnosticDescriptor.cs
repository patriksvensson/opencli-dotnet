#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

namespace OpenCli.Diagnostics;

[PublicAPI]
#if OPENCLI_VISIBILITY_INTERNAL
internal
#else
public
#endif
    sealed class OpenCliDiagnosticDescriptor
{
    public string? Code { get; }
    public string Summary { get; }
    public string Message { get; }
    public OpenCliSeverity Severity { get; }

    public OpenCliDiagnosticDescriptor(string? code, OpenCliSeverity severity, string summary, string message)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Severity = severity;
        Summary = summary;
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public OpenCliDiagnostic ToDiagnostic(OpenCliLocation? span)
    {
        return new OpenCliDiagnostic(Severity, Summary, Message) { Code = Code, Location = span, };
    }
}