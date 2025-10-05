using System.Collections.Immutable;
using OpenCli.Extensions.Analyzer.Internal;
using OpenCli.Extensions.Analyzer.Internal.Visitors;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public class NamingCaseAnalyzer : IOpenCliAnalyzer
{
    public const string OCA1000 = "OCA1000";
    public const string OCA1001 = "OCA1001";
    public const string OCA1002 = "OCA1002";
    public const string PreferredCaseKey = $"opencli_diagnostic.preferred_case";

    public static DiagnosticDescriptor CommandNamingCaseRule { get; } = new DiagnosticDescriptor(
        OCA1000,
        "Use correct naming case for command",
        "Command name {0} does not match preferred case.",
        DiagnosticSeverity.Suggestion);
    public static DiagnosticDescriptor ArgumentNamingCaseRule { get; } = new DiagnosticDescriptor(
        OCA1001,
        "Use correct naming case for argument",
        "Argument name {0} does not match preferred case.",
        DiagnosticSeverity.Suggestion);
    public static DiagnosticDescriptor OptionNamingCaseRule { get; } = new DiagnosticDescriptor(
        OCA1002,
        "Use correct naming case for option",
        "Option name {0} does not match preferred case.",
        DiagnosticSeverity.Suggestion);

    public ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [CommandNamingCaseRule, ArgumentNamingCaseRule, OptionNamingCaseRule];

    public void Analyze(OpenCliAnalyzeContext context)
    {
        // TODO: Options and arguments can start with '-' and '--'. Need to handle this
        var option = context.OptionProvider.GetOption(PreferredCaseKey);
        var preferredCase = option is null ? NameCaseMatcher.DefaultCase : NameCaseMatcher.Parse(option);
        var visitor = new CommandNameAnalyzerVisitor(preferredCase, context.DiagnosticCollector);
        visitor.Visit(context.Document);
    }

    private class CommandNameAnalyzerVisitor(NameCaseMatcher preferredCase, DiagnosticCollector diagnosticCollector) : OpenCliSpecVisitor
    {
        protected override void Visit(OpenCliCommand command)
        {
            if (!preferredCase.Regex.IsMatch(command.Name))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(CommandNamingCaseRule, command.Name));
            }

            base.Visit(command);
        }

        protected override void Visit(OpenCliArgument argument)
        {
            if (!preferredCase.Regex.IsMatch(argument.Name))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(ArgumentNamingCaseRule, argument.Name));
            }

            base.Visit(argument);
        }

        protected override void Visit(OpenCliOption option)
        {
            if (!preferredCase.Regex.IsMatch(option.Name))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(OptionNamingCaseRule, option.Name));
            }

            base.Visit(option);
        }
    }
}