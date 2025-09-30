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
sealed class OpenCliContact
{
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Email { get; set; }
}