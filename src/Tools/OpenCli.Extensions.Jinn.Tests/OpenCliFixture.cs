namespace Jinn.OpenCli.Tests;

public sealed class OpenCliFixture(Action<RootCommand> config)
{
    private readonly Action<RootCommand> _config =
        config ?? throw new ArgumentNullException(nameof(config));

    public async Task<(int ExitCode, string Output)> Run(
        string[]? args = null,
        OpenCliSettings? settings = null)
    {
        var output = new StringWriter();
        var command = new RootCommand();

        command.AddOpenCli(settings, (_, json) => output.Write(json));
        _config(command);

        var exitCode = await command.Invoke(args ?? ["--opencli"]);
        return (exitCode, output.ToString());
    }
}