namespace OpenCli.Extensions.Analyzer;

public class OptionProviderBuilder()
{
    private readonly IniParser _parser = new IniParser();
    private readonly Dictionary<string, string> _configurations = new Dictionary<string, string>();

    public OptionProviderBuilder AddConfigurationFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File {path} was not found");
        }

        var content = File.ReadAllLines(path);
        var result = _parser.Parse(content);

        foreach (var item in result)
        {
            _configurations[item.Key] = item.Value;
        }

        return this;
    }

    public OptionProvider Build()
    {
        return new OptionProvider(_configurations);
    }
}