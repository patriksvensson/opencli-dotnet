namespace System.CommandLine.OpenCli;

internal sealed class OpenCliDirective : Directive
{
    public OpenCliDirective(OpenCliSettings? settings = null)
        : base("opencli")
    {
        Action = new OpenCliAction(settings);
    }
}