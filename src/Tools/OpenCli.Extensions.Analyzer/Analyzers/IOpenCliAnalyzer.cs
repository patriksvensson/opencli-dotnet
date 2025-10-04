using OpenCli.Extensions.Analyzer.Internal;

namespace OpenCli.Extensions.Analyzer.Analyzers;

public interface IOpenCliAnalyzer
{
    public string Id { get; }

    void Analyze(OpenCliAnalyzeContext context);
}