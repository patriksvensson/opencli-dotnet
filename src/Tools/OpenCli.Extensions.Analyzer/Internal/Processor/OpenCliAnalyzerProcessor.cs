using OpenCli.Extensions.Analyzer.Analyzers;

namespace OpenCli.Extensions.Analyzer.Internal;

public class OpenCliAnalyzerProcessor(OpenCliAnalyzerConfig analyzerConfig)
{
    private readonly OpenCliAnalyzerSelector _analyzerSelector = new OpenCliAnalyzerSelector();

    public DiagnosticCollector Process(OpenCliDocument document)
    {
        IReadOnlyCollection<IOpenCliAnalyzer> analyzers = _analyzerSelector.GetAnalyzers(analyzerConfig);
        var diagnosticCollector = new DiagnosticCollector();
        var context = new OpenCliAnalyzeContext(document, analyzerConfig, diagnosticCollector);

        foreach (var analyzer in analyzers)
        {
            analyzer.Analyze(context);
        }

        diagnosticCollector = diagnosticCollector.UpdateSeverities(analyzerConfig);

        return diagnosticCollector;
    }
}