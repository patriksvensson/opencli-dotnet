using System.CommandLine.OpenCli;

namespace System.CommandLine;

[PublicAPI]
public static class CommandExtensions
{
    public static Command AddOpenCli(
        this RootCommand command,
        OpenCliSettings? settings = null,
        bool addDirective = true)
    {
        command.Options.Add(new OpenCliOption(settings));

        if (addDirective)
        {
            command.Directives.Add(new OpenCliDirective(settings));
        }

        return command;
    }
}