using OpenCli.Extensions.Analyzer.Analyzers;

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
            var option = optionProvider.GetOption($"opencli_diagnostic.{analyzer.Id}.severity");
            if (option is null)
            {
                continue;
            }

            if (!Enum.TryParse<OpenCliAnalyzerSeverity>(option, true, out var result))
            {
                continue;
            }

            if (result == OpenCliAnalyzerSeverity.Error
                || result == OpenCliAnalyzerSeverity.Warning
                || result == OpenCliAnalyzerSeverity.Suggestion
                || result == OpenCliAnalyzerSeverity.Silent)
            {
                filteredAnalyzers.Add(analyzer);
            }
        }

        return filteredAnalyzers;
    }
}