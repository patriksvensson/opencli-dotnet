namespace System.CommandLine.OpenCli;

internal sealed class OpenCliOption : Option<bool>
{
    public OpenCliOption(OpenCliSettings? settings = null)
        : base(name: "--opencli", [])
    {
        Hidden = true;
        Action = new OpenCliAction(settings);
        Arity = ArgumentArity.Zero;
    }
}