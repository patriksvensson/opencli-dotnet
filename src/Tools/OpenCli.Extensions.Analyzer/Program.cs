using System.ComponentModel;
using OpenCli.Extensions.Analyzer;
using OpenCli.Extensions.Analyzer.Analyzers;
using OpenCli.Extensions.Analyzer.Internal;
using OpenCli.Extensions.Analyzer.Internal.Report;
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

            [CommandOption("--configuration")]
            public string? Configuration { get; init; }

            [CommandOption("--report")]
            public string? ReportPath { get; init; }
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

            var optionProviderReader = new OptionProviderReader(new IniParser(), settings.Configuration);
            var optionProvider = optionProviderReader.Read();

            IReadOnlyCollection<IOpenCliAnalyzer> analyzers = new OpenCliAnalyzerSelector().GetAnalyzers(optionProvider);
            var diagnosticCollector = new DiagnosticCollector();

            foreach (var analyzer in analyzers)
            {
                analyzer.Analyze(new OpenCliAnalyzeContext(openCliParseResult.Document, optionProvider, diagnosticCollector));
            }

            IReporter reporter = settings.ReportPath is not null ? new JsonReporter(settings.ReportPath) : new TerminalReporter();
            reporter.Report(diagnosticCollector.Diagnostics);

            return diagnosticCollector.Diagnostics.Count == 0 ? 0 : 1;
        }
    }
}