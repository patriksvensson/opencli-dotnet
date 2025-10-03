namespace OpenCli.Extensions.Analyzer.Internal;

public class DiagnosticCollector
{
    public List<Diagnostic> Diagnostics { get; } = new List<Diagnostic>();

    // TODO: Support diagnostic location
    public void AddDiagnostic(Diagnostic diagnostic)
    {
        Diagnostics.Add(diagnostic);
    }
}