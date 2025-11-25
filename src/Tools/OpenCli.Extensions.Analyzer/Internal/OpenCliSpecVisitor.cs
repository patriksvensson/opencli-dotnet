namespace OpenCli.Extensions.Analyzer.Internal.Visitors;

public abstract class OpenCliSpecVisitor
{
    public void Visit(OpenCliDocument document)
    {
        foreach (var command in document.Commands)
        {
            Visit(command);
        }

        foreach (var argument in document.Arguments)
        {
            Visit(argument);
        }

        foreach (var option in document.Options)
        {
            Visit(option);
        }
    }

    protected virtual void Visit(OpenCliCommand command)
    {
        foreach (var argument in command.Arguments)
        {
            Visit(argument);
        }

        foreach (var option in command.Options)
        {
            Visit(option);
        }

        foreach (var subcommand in command.Commands)
        {
            Visit(subcommand);
        }
    }

    protected virtual void Visit(OpenCliArgument argument)
    {
    }

    protected virtual void Visit(OpenCliOption argument)
    {
    }
}