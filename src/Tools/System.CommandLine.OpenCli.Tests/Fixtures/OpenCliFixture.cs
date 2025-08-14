namespace System.CommandLine.OpenCli.Tests.Fixtures;

public sealed class OpenCliFixture(Action<RootCommand> config)
{
    private readonly Action<RootCommand> _config =
        config ?? throw new ArgumentNullException(nameof(config));

    public (int ExitCode, string Output) Run(
        string[]? args = null,
        OpenCliSettings? settings = null)
    {
        var command = new RootCommand();
        command.AddOpenCli(settings);
        _config(command);

        // Parse
        var parseResult = command.Parse(args ?? ["--opencli"]);

        // Invoke
        var output = new StringWriter();
        var exitCode = parseResult.Invoke(new InvocationConfiguration
        {
            Output = output,
        });

        return (exitCode, output.ToString());
    }
}