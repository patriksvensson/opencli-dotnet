using System.Text.Json;

namespace OpenCli.Extensions.Analyzer.Internal.Report;

public class JsonReporter(string filePath) : IReporter
{
    public void Report(List<Diagnostic> diagnostics)
    {
        CreateParentDiretory();

        // TODO: Support SARIF format
        var content = JsonSerializer.Serialize(diagnostics);
        File.WriteAllText(filePath, content);
    }

    private void CreateParentDiretory()
    {
        var parentDirectory = new FileInfo(filePath).Directory;
        if (parentDirectory is not null && !Directory.Exists(parentDirectory.FullName))
        {
            Directory.CreateDirectory(parentDirectory.FullName);
        }
    }
}