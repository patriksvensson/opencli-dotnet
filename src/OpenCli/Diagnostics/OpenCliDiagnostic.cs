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
    sealed class OpenCliDiagnostic
{
    public string? Code { get; init; }
    public string Summary { get; }
    public string Message { get; }
    public OpenCliSeverity Severity { get; }
    public OpenCliLocation? Location { get; init; }

    internal OpenCliDiagnostic(OpenCliSeverity severity, string summary, string message)
    {
        Severity = severity;
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}