using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public class TitleWithoutExtensionsAnalyzer : IOpenCliAnalyzer
{
    public const string Id = "OCA0001";
    public const string Message = "Don't use executable's name as title.";
    public const string ExtensionConfigurationKey = $"opencli_diagnostic.{Id}.extensions";
    private static readonly IReadOnlyCollection<string> _extensionConfigurationDefault = [".dll", ".exe"];

    public void Analyze(OpenCliAnalyzeContext context)
    {
        var extensions = context.OptionProvider.GetOptionList(ExtensionConfigurationKey) ?? _extensionConfigurationDefault;
        foreach (var extension in extensions)
        {
            if (context.Document.Info.Title.EndsWith(extension))
            {
                context.DiagnosticCollector.AddDiagnostic(new Diagnostic(Id, Message));
                return;
            }
        }
    }
}