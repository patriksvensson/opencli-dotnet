using System.ComponentModel;
using OpenCli.Extensions.Analyzer;
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

            [CommandOption("--severity")]
            public DiagnosticSeverity? Severity { get; init; }
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

            var optionProvider = LoadConfiguration(settings);
            var processor = new OpenCliAnalyzerProcessor(optionProvider);
            var diagnosticCollector = processor.Process(openCliParseResult.Document);

            var diagnostics = diagnosticCollector.Diagnostics;
            if (settings.Severity is not null)
            {
                diagnostics = diagnostics.Where(d => d.Severity <= settings.Severity).ToList();
            }

            IReporter reporter = settings.ReportPath is not null ? new JsonReporter(settings.ReportPath) : new TerminalReporter();
            reporter.Report(diagnostics);

            return diagnosticCollector.Diagnostics.Count == 0 ? 0 : 1;
        }

        private static OptionProvider LoadConfiguration(Settings settings)
        {
            var optionProviderBuilder = new OptionProviderBuilder();
            if (settings.Configuration is not null)
            {
                optionProviderBuilder.AddConfigurationFile(settings.Configuration);
            }

            return optionProviderBuilder.Build();
        }
    }
}