using System.ComponentModel;
using OpenCli.Extensions.Analyzer;
using OpenCli.Extensions.Analyzer.Analyzers;
using OpenCli.Extensions.Analyzer.Internal;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp<AnalyzeCommand>();
return app.Run(args);

namespace OpenCli.Extensions.Analyzer
{
    internal sealed class AnalyzeCommand : Command<AnalyzeCommand.Settings>
    {
        public sealed class Settings : CommandSettings
        {
            [Description("Path to open-cli spec")]
            [CommandArgument(0, "[spec-path]")]
            public string SpecPath { get; init; } = null!;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            var specContent = File.ReadAllText(settings.SpecPath);
            var openCliParseResult = OpenCliDocument.Parse(specContent).Result;
            if (openCliParseResult.HasErrors)
            {
                AnsiConsole.WriteLine("Spec contains errors. Fix them before run analysis.");
                return 1;
            }

            // TODO: Support configuration via config file
            var optionProvider = new OptionProvider();

            // TODO: Support analyzers enabling/disabling
            IReadOnlyCollection<IOpenCliAnalyzer> analyzers =
            [
                new TitleWithoutExtensionsAnalyzer(),
                new CommandNameCaseAnalyzer()
            ];
            var diagnosticCollector = new DiagnosticCollector();

            foreach (var analyzer in analyzers)
            {
                analyzer.Analyze(new OpenCliAnalyzeContext(openCliParseResult.Document, optionProvider, diagnosticCollector));
            }

            // TODO: Support SARIF output
            AnsiConsole.WriteLine($"Found {diagnosticCollector.Diagnostics.Count} diagnostics");
            foreach (var diagnosticCollectorDiagnostic in diagnosticCollector.Diagnostics)
            {
                AnsiConsole.WriteLine($"{diagnosticCollectorDiagnostic.Id}: {diagnosticCollectorDiagnostic.Message}");
            }

            return diagnosticCollector.Diagnostics.Count == 0 ? 0 : 1;
        }
    }
}