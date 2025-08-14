using System.CommandLine.Invocation;

namespace System.CommandLine.OpenCli;

internal sealed class OpenCliAction(OpenCliSettings? settings = null) : SynchronousCommandLineAction
{
    public override int Invoke(ParseResult parseResult)
    {
        var json = OpenCliGenerator.Generate(
            settings ?? new OpenCliSettings(),
            parseResult);

        parseResult.InvocationConfiguration.Output.Write(json);
        return 0;
    }
}