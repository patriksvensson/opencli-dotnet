namespace OpenCli;

[PublicAPI]
public sealed class OpenCliDocument
{
    public required string OpenCli { get; init; }
    public required OpenCliInfo Info { get; init; }
    public OpenCliConventions? Conventions { get; set; }
    public List<OpenCliArgument> Arguments { get; set; } = [];
    public List<OpenCliOption> Options { get; set; } = [];
    public List<OpenCliCommand> Commands { get; set; } = [];
    public List<OpenCliExitCode> ExitCodes { get; set; } = [];
    public List<string> Examples { get; set; } = [];
    public bool Interactive { get; set; }
    public List<OpenCliMetadata> Metadata { get; set; } = [];
}