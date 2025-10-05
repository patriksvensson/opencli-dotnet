using System.Collections.Immutable;
using OpenCli.Extensions.Analyzer.Internal;
using OpenCli.Extensions.Analyzer.Internal.Visitors;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public class MissedDescriptionAnalyzer : IOpenCliAnalyzer
{
    public const string OCA0002 = nameof(OCA0002);

    public static DiagnosticDescriptor Rule { get; } = new DiagnosticDescriptor(
        OCA0002,
        "Add description for commands/arguemtns/options",
        "Description was not provided for {0}",
        DiagnosticSeverity.Suggestion);

    public ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [Rule];

    public void Analyze(OpenCliAnalyzeContext context)
    {
        var visitor = new DescriptionVisitor(context.DiagnosticCollector);
        visitor.Visit(context.Document);
    }

    private class DescriptionVisitor(DiagnosticCollector diagnosticCollector) : OpenCliSpecVisitor
    {
        protected override void Visit(OpenCliCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Description))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(Rule, command.Name));
            }

            base.Visit(command);
        }

        protected override void Visit(OpenCliArgument argument)
        {
            if (string.IsNullOrWhiteSpace(argument.Description))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(Rule, argument.Name));
            }

            base.Visit(argument);
        }

        protected override void Visit(OpenCliOption option)
        {
            if (string.IsNullOrWhiteSpace(option.Description))
            {
                diagnosticCollector.AddDiagnostic(Diagnostic.Create(Rule, option.Name));
            }

            base.Visit(option);
        }
    }
}