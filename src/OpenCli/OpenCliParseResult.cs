using OpenCli.Diagnostics;

namespace OpenCli;

[PublicAPI]
public sealed class OpenCliParseResult
{
    public required OpenCliDocument? Document { get; init; }
    public required DiagnosticsCollection Diagnostics { get; init; }

    [MemberNotNullWhen(false, nameof(Document))]
    public bool HasErrors => Diagnostics.HasErrors;
}