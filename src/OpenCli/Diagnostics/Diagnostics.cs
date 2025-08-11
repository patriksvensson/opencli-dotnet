namespace OpenCli;

[PublicAPI]
public sealed class Diagnostics : List<Diagnostic>
{
    public bool HasErrors => this.Any(x => x.Severity == Severity.Error);

    public Diagnostics()
    {
    }

    public Diagnostics(IEnumerable<Diagnostic> diagnostics)
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

    public Diagnostics Merge(Diagnostics? other)
    {
        return other == null
            ? this
            : new Diagnostics(this.Concat(other));
    }
}