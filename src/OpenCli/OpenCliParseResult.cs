#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using OpenCli.Diagnostics;

namespace OpenCli;

[PublicAPI]
#if OPENCLI_VISIBILITY_INTERNAL
internal
#else
public
#endif
sealed class OpenCliParseResult
{
    public required OpenCliDocument? Document { get; init; }
    public required OpenCliDiagnosticsCollection Diagnostics { get; init; }

    [MemberNotNullWhen(false, nameof(Document))]
    public bool HasErrors => Diagnostics.HasErrors;
}