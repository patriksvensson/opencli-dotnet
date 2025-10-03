using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public interface IOpenCliAnalyzer
{
    void Analyze(OpenCliAnalyzeContext context);
}