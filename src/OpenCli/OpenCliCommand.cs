#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

namespace OpenCli;

[PublicAPI]
#if OPENCLI_VISIBILITY_INTERNAL
internal
#else
public
#endif
sealed class OpenCliCommand
{
    public required string Name { get; init; }
    public List<string> Aliases { get; set; } = [];
    public List<OpenCliOption> Options { get; set; } = [];
    public List<OpenCliArgument> Arguments { get; set; } = [];
    public List<OpenCliCommand> Commands { get; set; } = [];
    public List<OpenCliExitCode> ExitCodes { get; set; } = [];
    public string? Description { get; set; }
    public bool? Hidden { get; set; }
    public List<string> Examples { get; set; } = [];
    public bool? Interactive { get; set; }
    public List<OpenCliMetadata> Metadata { get; set; } = [];
}