namespace OpenCli.Internal;

internal static class OpenCliMapper
{
    public static JsonModel.DocumentJson Map(OpenCliDocument model)
    {
        return new JsonModel.DocumentJson
        {
            OpenCli = model.OpenCli,
            Info = MapOptional(model.Info, MapInfo),
            Conventions = MapOptional(model.Conventions, MapConventions),
            Arguments = MapList(model.Arguments, MapArgument),
            Options = MapList(model.Options, MapOption),
            Commands = MapList(model.Commands, MapCommand),
            ExitCodes = MapList(model.ExitCodes, MapExitCode),
            Examples = MapList(model.Examples),
            Interactive = model.Interactive,
            Metadata = MapList(model.Metadata, MapMetadata),
        };
    }

    private static JsonModel.InfoJson MapInfo(OpenCliInfo model)
    {
        return new JsonModel.InfoJson
        {
            Title = model.Title,
            Summary = model.Summary,
            Description = model.Description,
            Contact = MapOptional(model.Contact, MapContact),
            License = MapOptional(model.License, MapLicense),
            Version = model.Version,
        };
    }

    private static JsonModel.ConventionsJson MapConventions(OpenCliConventions model)
    {
        return new JsonModel.ConventionsJson
        {
            GroupOptions = model.GroupOptions,
            OptionArgumentSeparator = model.OptionArgumentSeparator,
        };
    }

    private static JsonModel.ArgumentJson MapArgument(OpenCliArgument model)
    {
        return new JsonModel.ArgumentJson
        {
            Name = model.Name,
            Required = model.Required,
            Arity = MapOptional(model.Arity, MapArity),
            AcceptedValues = MapList(model.AcceptedValues),
            Group = model.Group,
            Description = model.Description,
            Hidden = model.Hidden,
            Metadata = MapList(model.Metadata, MapMetadata),
        };
    }

    private static JsonModel.OptionJson MapOption(OpenCliOption model)
    {
        return new JsonModel.OptionJson
        {
            Name = model.Name,
            Required = model.Required,
            Aliases = MapList(model.Aliases),
            Arguments = MapList(model.Arguments, MapArgument),
            Group = model.Group,
            Description = model.Description,
            Recursive = model.Recursive,
            Hidden = model.Hidden,
            Metadata = MapList(model.Metadata, MapMetadata),
        };
    }

    private static JsonModel.CommandJson MapCommand(OpenCliCommand model)
    {
        return new JsonModel.CommandJson
        {
            Name = model.Name,
            Aliases = MapList(model.Aliases),
            Options = MapList(model.Options, MapOption),
            Arguments = MapList(model.Arguments, MapArgument),
            Commands = MapList(model.Commands, MapCommand),
            ExitCodes = MapList(model.ExitCodes, MapExitCode),
            Description = model.Description,
            Hidden = model.Hidden,
            Examples = model.Examples,
            Interactive = model.Interactive,
            Metadata = MapList(model.Metadata, MapMetadata),
        };
    }

    private static JsonModel.ExitCodeJson MapExitCode(OpenCliExitCode model)
    {
        return new JsonModel.ExitCodeJson
        {
            Code = model.Code,
            Description = model.Description,
        };
    }

    private static JsonModel.MetadataJson MapMetadata(OpenCliMetadata model)
    {
        return new JsonModel.MetadataJson
        {
            Name = model.Name,
            Value = model.Value,
        };
    }

    private static JsonModel.ContactJson MapContact(OpenCliContact model)
    {
        return new JsonModel.ContactJson
        {
            Name = model.Name,
            Url = model.Url,
            Email = model.Email,
        };
    }

    private static JsonModel.LicenseJson MapLicense(OpenCliLicense model)
    {
        return new JsonModel.LicenseJson
        {
            Name = model.Name,
            Identifier = model.Identifier,
        };
    }

    private static JsonModel.ArityJson MapArity(OpenCliArity model)
    {
        return new JsonModel.ArityJson
        {
            Minimum = model.Minimum,
            Maximum = model.Maximum,
        };
    }

    private static TTo? MapOptional<TFrom, TTo>(TFrom? from, Func<TFrom, TTo> func)
        where TTo : class
    {
        return from == null ? null : func(from);
    }

    private static List<T>? MapList<T>(List<T>? from)
    {
        return from?.Count == 0 ? null : from;
    }

    private static List<TTo>? MapList<TFrom, TTo>(List<TFrom>? from, Func<TFrom, TTo> func)
    {
        if (from == null)
        {
            return null;
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

        if (result.Count == 0)
        {
            return null;
        }

        return result;
    }
}