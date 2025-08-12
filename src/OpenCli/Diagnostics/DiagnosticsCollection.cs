namespace OpenCli.Diagnostics;

[PublicAPI]
public sealed class DiagnosticsCollection : List<Diagnostic>
{
    public bool HasErrors => this.Any(x => x.Severity == Severity.Error);

    public DiagnosticsCollection()
    {
    }

    public DiagnosticsCollection(IEnumerable<Diagnostic> diagnostics)
        : base(diagnostics)
    {
    }

    public Diagnostic Add(DiagnosticDescriptor descriptor)
    {
        var diagnostic = descriptor.ToDiagnostic(null);
        Add(diagnostic);
        return diagnostic;
    }

    public Diagnostic Add(Location? location, DiagnosticDescriptor descriptor)
    {
        var diagnostic = descriptor.ToDiagnostic(location);
        Add(diagnostic);
        return diagnostic;
    }

    public DiagnosticsCollection Merge(DiagnosticsCollection? other)
    {
        return other == null
            ? this
            : new DiagnosticsCollection(this.Concat(other));
    }
}