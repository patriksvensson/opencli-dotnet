using OpenCli.Extensions.Analyzer.Analyzers;
using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer;

public class OpenCliAnalyzerSelector
{
    public IReadOnlyCollection<IOpenCliAnalyzer> GetAnalyzers(OpenCliAnalyzerConfig optionProvider)
    {
        IReadOnlyCollection<IOpenCliAnalyzer> analyzers =
            [
                new TitleWithoutExtensionsAnalyzer(),
                new MissedDescriptionAnalyzer(),
                new NamingCaseAnalyzer()
            ];

        List<IOpenCliAnalyzer> filteredAnalyzers = [];
        foreach (var analyzer in analyzers)
        {
            var shouldBeEnabled = analyzer.SupportedDiagnostics.Any(d => InEnabled(d, optionProvider));
            if (shouldBeEnabled)
            {
                filteredAnalyzers.Add(analyzer);
            }
        }

        return filteredAnalyzers;
    }

    private bool InEnabled(DiagnosticDescriptor descriptor, OpenCliAnalyzerConfig analyzerConfig)
    {
        var severity = analyzerConfig.GetSeverity(descriptor.Id);
        return InEnabled(severity ?? descriptor.DefaultSeverity);
    }

    private bool InEnabled(DiagnosticSeverity severity)
    {
        return severity == DiagnosticSeverity.Error
                || severity == DiagnosticSeverity.Warning
                || severity == DiagnosticSeverity.Suggestion
                || severity == DiagnosticSeverity.Silent;
    }
}