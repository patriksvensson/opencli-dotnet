using OpenCli;
using OpenCliDocumentOption = OpenCli.OpenCliOption;

namespace System.CommandLine.OpenCli;

internal sealed class OpenCliGenerator
{
    public static string Generate(
        OpenCliSettings settings,
        ParseResult parseResult)
    {
        var document = new OpenCliDocument
        {
            OpenCli = "draft",
            Info = new OpenCliInfo
            {
                Title = settings.Title ?? RootCommand.ExecutableName,
                Version = settings.Version ?? ExecutableVersion.GetExecutableVersion(),
            },
            Commands = CreateCommands(parseResult.CommandResult.Command.Subcommands),
            Arguments = CreateArguments(parseResult.CommandResult.Command.Arguments),
            Options = CreateOptions(parseResult.CommandResult.Command.Options),
        };

        return document.Write();
    }

    private static List<OpenCliCommand> CreateCommands(IList<Command> commands)
    {
        var result = new List<OpenCliCommand>();

        foreach (var command in commands.OrderBy(o => o.Name, StringComparer.OrdinalIgnoreCase))
        {
            result.Add(new OpenCliCommand
            {
                Name = command.Name,
                Aliases = [..command.Aliases.OrderBy(str => str)],
                Commands = CreateCommands(command.Subcommands),
                Arguments = CreateArguments(command.Arguments),
                Options = CreateOptions(command.Options),
                Description = command.Description,
                Hidden = command.Hidden ? true : null,
            });
        }

        return result;
    }

    private static List<OpenCliArgument> CreateArguments(IList<Argument> arguments)
    {
        var result = new List<OpenCliArgument>();

        foreach (var argument in arguments)
        {
            var metadata = default(List<OpenCliMetadata>);
            if (argument.ValueType != typeof(void) &&
                argument.ValueType != typeof(bool))
            {
                // TODO: Ignore bool metadata?
                metadata =
                [
                    new OpenCliMetadata
                    {
                        Name = "ClrType",
                        Value = argument.ValueType.ToCliTypeString(),
                    },
                ];
            }

            result.Add(new OpenCliArgument
            {
                Name = argument.Name,
                Arity = new OpenCliArity
                {
                    Minimum = argument.Arity.MinimumNumberOfValues,
                    Maximum = argument.Arity.MaximumNumberOfValues,
                },
                Description = argument.Description,
                Hidden = argument.Hidden ? true : null,
                Metadata = metadata,
            });
        }

        return result;
    }

    private static List<OpenCliDocumentOption> CreateOptions(IList<Option> options)
    {
        var result = new List<OpenCliDocumentOption>();

        foreach (var option in options.OrderBy(o => o.Name, StringComparer.OrdinalIgnoreCase))
        {
            var arguments = new List<OpenCliArgument>();
            if (option.ValueType != typeof(void) &&
                option.ValueType != typeof(bool))
            {
                arguments.Add(new OpenCliArgument
                {
                    Name = option.HelpName ?? "VALUE",
                    Arity = new OpenCliArity
                    {
                        Minimum = option.Arity.MinimumNumberOfValues,
                        Maximum = option.Arity.MaximumNumberOfValues == ArgumentArity.ZeroOrMore.MaximumNumberOfValues
                            ? null
                            : option.Arity.MaximumNumberOfValues,
                    },
                    Metadata =
                    [
                        new OpenCliMetadata
                        {
                            Name = "ClrType",
                            Value = option.ValueType.ToCliTypeString(),
                        },
                    ],
                });
            }

            var optionMetadata = default(List<OpenCliMetadata>);
            if (arguments.Count == 0 && option.ValueType != typeof(bool) &&
                option.ValueType != typeof(void))
            {
                optionMetadata =
                [
                    new OpenCliMetadata
                    {
                        Name = "ClrType",
                        Value = option.ValueType.ToCliTypeString(),
                    },
                ];
            }

            result.Add(new OpenCliDocumentOption
            {
                Name = option.Name,
                Required = option.Required ? true : null,
                Aliases = [..option.Aliases.OrderBy(str => str),],
                Arguments = arguments,
                Description = option.Description,
                Group = null,
                Hidden = option.Hidden ? true : null,
                Recursive = option.Recursive ? true : null,
                Metadata = optionMetadata,
            });
        }

        return result;
    }
}