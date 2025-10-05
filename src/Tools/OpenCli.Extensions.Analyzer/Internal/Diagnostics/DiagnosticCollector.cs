namespace OpenCli.Extensions.Analyzer.Internal;

public class DiagnosticCollector
{
    public List<Diagnostic> Diagnostics { get; } = new List<Diagnostic>();

    public void AddDiagnostic(Diagnostic diagnostic)
    {
        Diagnostics.Add(diagnostic);
    }
}