using OpenCli;

namespace Jinn.OpenCli;

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
                Title = settings.Title ?? "?",
                Version = settings.Version ?? "?",
            },
            Commands = CreateCommands(parseResult.ParsedCommand.CommandSymbol.Commands),
            Arguments = CreateArguments(parseResult.ParsedCommand.CommandSymbol.Arguments),
            Options = CreateOptions(parseResult.ParsedCommand.CommandSymbol.Options),
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
                Commands = CreateCommands(command.Commands),
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
                    Minimum = argument.Arity.Minimum,
                    Maximum = argument.Arity.Maximum,
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

        foreach (var option in options.OrderBy(o => o.Name.Name, StringComparer.OrdinalIgnoreCase))
        {
            var arguments = new List<OpenCliArgument>();
            if (option.ValueType != typeof(void) &&
                option.ValueType != typeof(bool))
            {
                arguments.Add(new OpenCliArgument
                {
                    Name = option.Name.Name,
                    Arity = new OpenCliArity
                    {
                        Minimum = option.Arity.Minimum,
                        Maximum = option.Arity.Maximum == Arity.ZeroOrMore.Maximum
                            ? null
                            : option.Arity.Maximum,
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
                Name = option.Name.Name,
                Required = option.IsRequired ? true : null,
                Aliases = [..option.Names.Select(x => x.Name).OrderBy(str => str)],
                Arguments = arguments,
                Description = option.Description,
                Group = null,
                Hidden = option.Hidden ? true : null,
                Recursive = null,
                Metadata = optionMetadata,
            });
        }

        return result;
    }
}