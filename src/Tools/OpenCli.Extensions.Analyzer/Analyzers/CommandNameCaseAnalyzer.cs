using System.Collections.Immutable;
using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public class CommandNameCaseAnalyzer : IOpenCliAnalyzer
{
    public const string OCA0002 = "OCA0002";
    public const string PreferredCaseKey = $"opencli_diagnostic.OCA0002.preferred_case";

    private const string KebabCase = "kebab-case";
    private const string PascalCase = "pascal-case";
    private const string SnakeCase = "snake-case";

    private static readonly NameCaseMatcher DefaultCase = NameCaseMatcher.KebabCase;

    public static DiagnosticDescriptor Rule { get; } = new DiagnosticDescriptor(
        OCA0002,
        "Use correct naming case for command",
        "Command name {0} does not match preferred case.",
        DiagnosticSeverity.Suggestion);

    public ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public void Analyze(OpenCliAnalyzeContext context)
    {
        var preferredCase = GetPreferredCase(context);

        foreach (OpenCliCommand command in context.Document.Commands)
        {
            AnalyzeCommand(context, command, preferredCase);
        }
    }

    private void AnalyzeCommand(OpenCliAnalyzeContext context, OpenCliCommand command, NameCaseMatcher preferredCase)
    {
        if (!preferredCase.Regex.IsMatch(command.Name))
        {
            context.DiagnosticCollector.AddDiagnostic(Diagnostic.Create(Rule, command.Name));
        }

        foreach (var openCliCommand in command.Commands)
        {
            AnalyzeCommand(context, openCliCommand, preferredCase);
        }
    }

    private NameCaseMatcher GetPreferredCase(OpenCliAnalyzeContext context)
    {
        var option = context.OptionProvider.GetOption(PreferredCaseKey);
        if (option is null)
        {
            return DefaultCase;
        }

        if (string.Equals(option, PascalCase, StringComparison.InvariantCultureIgnoreCase))
        {
            return NameCaseMatcher.PascalCase;
        }

        if (string.Equals(option, KebabCase, StringComparison.InvariantCultureIgnoreCase))
        {
            return NameCaseMatcher.KebabCase;
        }

        if (string.Equals(option, SnakeCase, StringComparison.InvariantCultureIgnoreCase))
        {
            return NameCaseMatcher.SnakeCase;
        }

        return DefaultCase;
    }
}