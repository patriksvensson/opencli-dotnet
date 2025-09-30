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
sealed class OpenCliConventions
{
    public bool? GroupOptions { get; set; }
    public string? OptionArgumentSeparator { get; set; }
}