using System.Collections.Immutable;
using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public interface IOpenCliAnalyzer
{
    ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }

    void Analyze(OpenCliAnalyzeContext context);
}