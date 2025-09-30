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
    readonly struct OpenCliLocation
{
    public int Row { get; init; }
    public int Column { get; init; }
}