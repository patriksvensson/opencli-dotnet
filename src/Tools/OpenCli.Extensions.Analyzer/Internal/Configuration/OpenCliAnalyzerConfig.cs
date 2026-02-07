namespace OpenCli.Extensions.Analyzer;

public class OpenCliAnalyzerConfig(Dictionary<string, string> values)
{
    public static OpenCliAnalyzerConfig Empty { get; } = new OpenCliAnalyzerConfig(new Dictionary<string, string>());

    public DiagnosticSeverity? GetSeverity(string ruleId)
    {
        var option = GetOption($"opencli_diagnostic.{ruleId}.severity");
        if (option == null)
        {
            return null;
        }

        if (Enum.TryParse<DiagnosticSeverity>(option, ignoreCase: true, out var result))
        {
            return result;
        }

        return null;
    }

    public string? GetOption(string key)
    {
        if (values.TryGetValue(key, out string? result))
        {
            return result;
        }

        return null;
    }

    public IReadOnlyCollection<string>? GetOptionList(string key)
    {
        var value = GetOption(key);
        if (value is null)
        {
            return null;
        }

        return value.Split(';');
    }
}