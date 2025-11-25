using Spectre.Console;

namespace OpenCli.Extensions.Analyzer.Internal.Report;

public class TerminalReporter : IReporter
{
    public void Report(List<Diagnostic> diagnostics)
    {
        AnsiConsole.WriteLine($"Found {diagnostics.Count} diagnostics");
        foreach (var diagnosticCollectorDiagnostic in diagnostics)
        {
            AnsiConsole.WriteLine($"{diagnosticCollectorDiagnostic.Id}: {diagnosticCollectorDiagnostic.Message}");
        }
    }
}