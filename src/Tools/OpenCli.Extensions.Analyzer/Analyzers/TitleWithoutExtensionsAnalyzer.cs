using System.Collections.Immutable;
using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public class TitleWithoutExtensionsAnalyzer : IOpenCliAnalyzer
{
    public const string OCA0001 = "OCA0001";
    public const string ExtensionConfigurationKey = $"opencli_diagnostic.OCA0001.extensions";
    private static readonly IReadOnlyCollection<string> _extensionConfigurationDefault = [".dll", ".exe"];

    public static DiagnosticDescriptor Rule { get; } = new DiagnosticDescriptor(
        OCA0001,
        "Don't use executable's name as title",
        "Application title {0} contains extension {1}.",
        DiagnosticSeverity.Suggestion);

    public ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public void Analyze(OpenCliAnalyzeContext context)
    {
        var extensions = context.OptionProvider.GetOptionList(ExtensionConfigurationKey) ?? _extensionConfigurationDefault;
        foreach (var extension in extensions)
        {
            if (context.Document.Info.Title.EndsWith(extension))
            {
                context.DiagnosticCollector.AddDiagnostic(Diagnostic.Create(Rule, context.Document.Info.Title, extension));
                return;
            }
        }
    }
}