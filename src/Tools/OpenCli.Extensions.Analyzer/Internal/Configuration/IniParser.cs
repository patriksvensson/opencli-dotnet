namespace OpenCli.Extensions.Analyzer;

public class IniParser
{
    public Dictionary<string, string> Parse(string[] content)
    {
        Dictionary<string, string> result = [];

        foreach (var line in content)
        {
            var parsedLine = ParseLine(line);
            if (parsedLine is not null)
            {
                result[parsedLine.Value.Key] = parsedLine.Value.Value;
            }
        }

        return result;
    }

    public (string Key, string Value)? ParseLine(string line)
    {
        if (!line.Contains('='))
        {
            return null;
        }

        var parts = line.Split('=', 2);
        return (parts[0].Trim(), parts[1].Trim());
    }
}