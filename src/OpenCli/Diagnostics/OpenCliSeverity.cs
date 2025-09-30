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
enum OpenCliSeverity
{
    Information = 0,
    Warning = 1,
    Error = 2,
}