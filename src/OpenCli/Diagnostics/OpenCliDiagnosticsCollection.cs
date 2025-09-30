#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

namespace OpenCli.Diagnostics;

[PublicAPI]
#if OPENCLI_VISIBILITY_INTERNAL
internal
#else
public
#endif
    sealed class OpenCliDiagnosticsCollection : List<OpenCliDiagnostic>
{
    public bool HasErrors => this.Any(x => x.Severity == OpenCliSeverity.Error);

    public OpenCliDiagnosticsCollection()
    {
    }

    public OpenCliDiagnosticsCollection(IEnumerable<OpenCliDiagnostic> diagnostics)
        : base(diagnostics)
    {
    }

    public OpenCliDiagnostic Add(OpenCliDiagnosticDescriptor descriptor)
    {
        var diagnostic = descriptor.ToDiagnostic(null);
        Add(diagnostic);
        return diagnostic;
    }

    public OpenCliDiagnostic Add(OpenCliLocation? location, OpenCliDiagnosticDescriptor descriptor)
    {
        var diagnostic = descriptor.ToDiagnostic(location);
        Add(diagnostic);
        return diagnostic;
    }

    public OpenCliDiagnosticsCollection Merge(OpenCliDiagnosticsCollection? other)
    {
        return other == null
            ? this
            : new OpenCliDiagnosticsCollection(this.Concat(other));
    }
}