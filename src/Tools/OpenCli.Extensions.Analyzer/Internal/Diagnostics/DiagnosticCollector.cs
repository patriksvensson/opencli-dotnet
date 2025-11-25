namespace OpenCli.Extensions.Analyzer.Internal;

public class DiagnosticCollector
{
    public List<Diagnostic> Diagnostics { get; }

    public DiagnosticCollector(List<Diagnostic> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public DiagnosticCollector()
        : this(new List<Diagnostic>())
    {
    }

    public void AddDiagnostic(Diagnostic diagnostic)
    {
        Diagnostics.Add(diagnostic);
    }

    public DiagnosticCollector UpdateSeverities(OpenCliAnalyzerConfig analyzerConfig)
    {
        var updatedDiagnostics = Diagnostics.Select(d => UpdateSeverity(d, analyzerConfig)).ToList();
        return new DiagnosticCollector(updatedDiagnostics);
    }

    private static Diagnostic UpdateSeverity(Diagnostic diagnostic, OpenCliAnalyzerConfig analyzerConfig)
    {
        var severity = analyzerConfig.GetSeverity(diagnostic.Id);
        if (severity is null)
        {
            return diagnostic;
        }

        return diagnostic with { Severity = severity.Value };
    }
}