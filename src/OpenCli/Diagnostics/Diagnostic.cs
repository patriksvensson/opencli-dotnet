namespace OpenCli;

[PublicAPI]
public sealed class Diagnostic
{
    public string? Code { get; init; }
    public string Summary { get; }
    public string Message { get; }
    public Severity Severity { get; }
    public Location? Location { get; init; }

    internal Diagnostic(Severity severity, string summary, string message)
    {
        Severity = severity;
        Summary = summary ?? throw new ArgumentNullException(nameof(summary));
        Message = message ?? throw new ArgumentNullException(nameof(message));
    }
}