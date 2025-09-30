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
sealed class OpenCliArity
{
    public int? Minimum { get; set; }
    public int? Maximum { get; set; }
}