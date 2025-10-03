using System.Text.RegularExpressions;

namespace OpenCli.Extensions.Analyzer.Internal;

public record NameCaseMatcher(string Name, Regex Regex)
{
    public static NameCaseMatcher KebabCase { get; } = new NameCaseMatcher("kebab-case", new Regex("^([a-z][a-z0-9]*)(-[a-z0-9]+)*$"));
    public static NameCaseMatcher PascalCase { get; } = new NameCaseMatcher("kebab-case", new Regex("^[A-Z][a-zA-Z0-9]+$"));
    public static NameCaseMatcher SnakeCase { get; } = new NameCaseMatcher("kebab-case", new Regex("^([a-z][a-z0-9]*)(_[a-z0-9]+)*$"));
}