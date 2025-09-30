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
sealed class OpenCliInfo
{
    public required string Title { get; init; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public OpenCliContact? Contact { get; set; }
    public OpenCliLicense? License { get; set; }
    public required string Version { get; init; }
}