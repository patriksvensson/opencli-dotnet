namespace OpenCli.Extensions.Analyzer;

public class OptionProviderReader(IniParser parser, string? path = null)
{
    public OptionProvider Read()
    {
        if (path is null)
        {
            return OptionProvider.Empty;
        }

        if (!File.Exists(path))
        {
            return OptionProvider.Empty;
        }

        var content = File.ReadAllLines(path);
        var result = parser.Parse(content);
        return new OptionProvider(result);
    }
}