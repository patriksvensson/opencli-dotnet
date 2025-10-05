using OpenCli.Extensions.Analyzer.Analyzers;
using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer;

public class OpenCliAnalyzerSelector
{
    public IReadOnlyCollection<IOpenCliAnalyzer> GetAnalyzers(OptionProvider optionProvider)
    {
        IReadOnlyCollection<IOpenCliAnalyzer> analyzers =
            [
                new TitleWithoutExtensionsAnalyzer(),
                new CommandNameCaseAnalyzer()
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

    private bool InEnabled(DiagnosticDescriptor descriptor, OptionProvider optionProvider)
    {
        var option = optionProvider.GetOption($"opencli_diagnostic.{descriptor.Id}.severity");
        if (option is not null)
        {
            if (!Enum.TryParse<DiagnosticSeverity>(option, true, out var result))
            {
                return InEnabled(result);
            }
        }

        return InEnabled(descriptor.DefaultSeverity);
    }

    private bool InEnabled(DiagnosticSeverity severity)
    {
        return severity == DiagnosticSeverity.Error
                || severity == DiagnosticSeverity.Warning
                || severity == DiagnosticSeverity.Suggestion
                || severity == DiagnosticSeverity.Silent;
    }
}