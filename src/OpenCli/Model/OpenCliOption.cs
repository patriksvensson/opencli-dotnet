namespace OpenCli;

public sealed class OpenCliOption
{
    public required string Name { get; init; }
    public bool? Required { get; set; }
    public List<string> Aliases { get; set; } = [];
    public List<OpenCliArgument>? Arguments { get; set; }
    public string? Group { get; set; }
    public string? Description { get; set; }
    public bool? Recursive { get; set; }
    public bool? Hidden { get; set; }
    public List<OpenCliMetadata>? Metadata { get; set; }
}