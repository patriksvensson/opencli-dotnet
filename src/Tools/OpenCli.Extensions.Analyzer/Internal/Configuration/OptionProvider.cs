namespace OpenCli.Extensions.Analyzer;

public class OptionProvider(Dictionary<string, string> values)
{
    public static OptionProvider Empty { get; } = new OptionProvider(new Dictionary<string, string>());

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