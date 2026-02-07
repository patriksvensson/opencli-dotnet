namespace OpenCli.Extensions.Analyzer.Internal.Report;

public interface IReporter
{
    void Report(List<Diagnostic> diagnostics);
}