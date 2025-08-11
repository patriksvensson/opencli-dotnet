namespace OpenCli;

[PublicAPI]
public sealed class OpenCliExitCode
{
    public int? Code { get; set; }
    public string? Description { get; set; }
}