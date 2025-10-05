using Microsoft.CodeAnalysis.Sarif;

namespace OpenCli.Extensions.Analyzer.Internal.Report;

public class SarifReporter(string filePath) : IReporter
{
    public void Report(List<Diagnostic> diagnostics)
    {
        CreateParentDirectory();

        var results = diagnostics.Select(MapDiagnosticToSarifResult).ToList();

        var assemblyName = typeof(AnalyzeCommand).Assembly.GetName();
        var run = new Run
        {
            Tool = new Tool
            {
                Driver = new ToolComponent
                {
                    Name = assemblyName.Name,
                    Version = typeof(AnalyzeCommand).Assembly.GetName().Version?.ToString() ?? "1.0.0",
                },
            },
            Results = results,
        };

        var log = new SarifLog
        {
            Version = SarifVersion.Current,
            Runs = new List<Run> { run },
        };

        log.Save(filePath);
    }

    private void CreateParentDirectory()
    {
        var parentDirectory = new FileInfo(filePath).Directory;
        if (parentDirectory is not null && !Directory.Exists(parentDirectory.FullName))
        {
            Directory.CreateDirectory(parentDirectory.FullName);
        }
    }

    private static Result MapDiagnosticToSarifResult(Diagnostic diagnostic)
    {
        return new Result
        {
            RuleId = diagnostic.Id,
            Message = new Message() { Text = diagnostic.Message, Id = diagnostic.Id },
            Level = MapSeverityToFailureLevel(diagnostic.Severity),
        };
    }

    private static FailureLevel MapSeverityToFailureLevel(DiagnosticSeverity severity)
    {
        return severity switch
        {
            DiagnosticSeverity.Error => FailureLevel.Error,
            DiagnosticSeverity.Warning => FailureLevel.Warning,
            DiagnosticSeverity.Suggestion => FailureLevel.Note,
            DiagnosticSeverity.Silent => FailureLevel.None,
            DiagnosticSeverity.None => FailureLevel.None,
            _ => throw new NotSupportedException(severity.ToString()),
        };
    }
}