namespace OpenCli.Extensions.Analyzer.Internal;

public class OptionProvider
{
    public string? GetOption(string key)
    {
        // TODO: Implement configuration reading from config file
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