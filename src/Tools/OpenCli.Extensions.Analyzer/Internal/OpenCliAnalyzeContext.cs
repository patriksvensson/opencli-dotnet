namespace OpenCli.Extensions.Analyzer.Internal;

public record OpenCliAnalyzeContext(OpenCliDocument Document, OptionProvider OptionProvider, DiagnosticCollector DiagnosticCollector);