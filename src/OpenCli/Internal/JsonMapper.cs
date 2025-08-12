namespace OpenCli.Internal;

internal static class JsonMapper
{
    public static OpenCliDocument Map(JsonModel.DocumentJson json)
    {
        return new OpenCliDocument
        {
            OpenCli = json.OpenCli!,
            Info = MapInfo(json.Info),
            Interactive = json.Interactive,
            Metadata = MapList(json.Metadata, MapMetadata),
            Commands = MapList(json.Commands, MapCommand),
            ExitCodes = MapList(json.ExitCodes, MapExitCode),
            Examples = json.Examples ?? [],
            Arguments = MapList(json.Arguments, MapArgument),
            Options = MapList(json.Options, MapOption),
            Conventions = MapOptional(json.Conventions, MapConventions),
        };
    }

    private static OpenCliInfo MapInfo(JsonModel.InfoJson? json)
    {
        if (json == null)
        {
            throw new InvalidOperationException("OpenCLI info is missing");
        }

        return new OpenCliInfo
        {
            Title = json.Title!,
            Summary = json.Summary,
            Description = json.Description,
            Contact = MapOptional(json.Contact, MapContact),
            License = MapOptional(json.License, MapLicense),
            Version = json.Version!,
        };
    }

    private static OpenCliArity MapArity(JsonModel.ArityJson json)
    {
        return new OpenCliArity
        {
            Minimum = json.Minimum,
            Maximum = json.Maximum,
        };
    }

    private static OpenCliContact MapContact(JsonModel.ContactJson json)
    {
        return new OpenCliContact
        {
            Name = json.Name,
            Url = json.Url,
            Email = json.Email,
        };
    }

    private static OpenCliLicense MapLicense(JsonModel.LicenseJson json)
    {
        return new OpenCliLicense
        {
            Name = json.Name,
            Identifier = json.Identifier,
        };
    }

    private static OpenCliConventions MapConventions(JsonModel.ConventionsJson json)
    {
        return new OpenCliConventions
        {
            GroupOptions = json.GroupOptions,
            OptionArgumentSeparator = json.OptionArgumentSeparator,
        };
    }

    private static OpenCliExitCode MapExitCode(JsonModel.ExitCodeJson json)
    {
        return new OpenCliExitCode
        {
            Code = json.Code,
            Description = json.Description,
        };
    }

    private static OpenCliMetadata MapMetadata(JsonModel.MetadataJson json)
    {
        return new OpenCliMetadata
        {
            Name = json.Name,
            Value = json.Value,
        };
    }

    private static OpenCliCommand MapCommand(JsonModel.CommandJson json)
    {
        return new OpenCliCommand
        {
            Name = json.Name!,
            Aliases = json.Aliases ?? [],
            Options = MapList(json.Options, MapOption),
            Arguments = MapList(json.Arguments, MapArgument),
            Commands = MapList(json.Commands, MapCommand),
            ExitCodes = MapList(json.ExitCodes, MapExitCode),
            Description = json.Description,
            Hidden = json.Hidden,
            Examples = json.Examples ?? [],
            Interactive = json.Interactive,
            Metadata = MapList(json.Metadata, MapMetadata),
        };
    }

    private static OpenCliOption MapOption(JsonModel.OptionJson json)
    {
        return new OpenCliOption
        {
            Name = json.Name!,
            Required = json.Required,
            Aliases = json.Aliases ?? [],
            Arguments = MapList(json.Arguments, MapArgument),
            Group = json.Group,
            Description = json.Description,
            Recursive = json.Recursive,
            Hidden = json.Hidden,
            Metadata = MapList(json.Metadata, MapMetadata),
        };
    }

    private static OpenCliArgument MapArgument(JsonModel.ArgumentJson json)
    {
        return new OpenCliArgument
        {
            Name = json.Name!,
            Required = json.Required,
            Arity = MapOptional(json.Arity, MapArity),
            AcceptedValues = json.AcceptedValues,
            Group = json.Group,
            Description = json.Description,
            Hidden = json.Hidden,
            Metadata = MapList(json.Metadata, MapMetadata),
        };
    }

    private static TTo? MapOptional<TFrom, TTo>(TFrom? from, Func<TFrom, TTo> func)
        where TTo : class
    {
        return from == null ? null : func(from);
    }

    private static List<TTo> MapList<TFrom, TTo>(List<TFrom>? from, Func<TFrom, TTo> func)
    {
        if (from == null)
        {
            return [];
        }

        var result = new List<TTo>();
        foreach (var item in from)
        {
            var mappedItem = func(item);
            if (mappedItem != null)
            {
                result.Add(mappedItem);
            }
        }

        return result;
    }
}