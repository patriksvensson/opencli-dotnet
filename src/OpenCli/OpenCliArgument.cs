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
sealed class OpenCliArgument
{
    public required string Name { get; init; }
    public bool? Required { get; set; }
    public OpenCliArity? Arity { get; set; }
    public List<string>? AcceptedValues { get; set; }
    public string? Group { get; set; }
    public string? Description { get; set; }
    public bool? Hidden { get; set; }
    public List<OpenCliMetadata>? Metadata { get; set; }
}