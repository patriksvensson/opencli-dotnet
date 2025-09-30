#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using System.Text.Json.Serialization;

namespace OpenCli.Internal;

internal static class JsonModel
{
    public sealed class DocumentJson
    {
        [JsonPropertyName("opencli")]
        public string? OpenCli { get; set; }

        [JsonPropertyName("info")]
        public InfoJson? Info { get; set; }

        [JsonPropertyName("conventions")]
        public ConventionsJson? Conventions { get; set; }

        [JsonPropertyName("arguments")]
        public List<ArgumentJson>? Arguments { get; set; }

        [JsonPropertyName("options")]
        public List<OptionJson>? Options { get; set; }

        [JsonPropertyName("commands")]
        public List<CommandJson>? Commands { get; set; }

        [JsonPropertyName("exitCodes")]
        public List<ExitCodeJson>? ExitCodes { get; set; }

        [JsonPropertyName("examples")]
        public List<string>? Examples { get; set; }

        [JsonPropertyName("interactive")]
        public bool? Interactive { get; set; }

        [JsonPropertyName("metadata")]
        public List<MetadataJson>? Metadata { get; set; }
    }

    public sealed class InfoJson
    {
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("contact")]
        public ContactJson? Contact { get; set; }

        [JsonPropertyName("license")]
        public LicenseJson? License { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }
    }

    public sealed class ContactJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }

    public sealed class LicenseJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("identifier")]
        public string? Identifier { get; set; }
    }

    public sealed class ConventionsJson
    {
        [JsonPropertyName("groupOptions")]
        public bool? GroupOptions { get; set; }

        [JsonPropertyName("optionArgumentSeparator")]
        public string? OptionArgumentSeparator { get; set; }
    }

    public sealed class CommandJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("aliases")]
        public List<string>? Aliases { get; set; }

        [JsonPropertyName("options")]
        public List<OptionJson>? Options { get; set; }

        [JsonPropertyName("arguments")]
        public List<ArgumentJson>? Arguments { get; set; }

        [JsonPropertyName("commands")]
        public List<CommandJson>? Commands { get; set; }

        [JsonPropertyName("exitCodes")]
        public List<ExitCodeJson>? ExitCodes { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("hidden")]
        public bool? Hidden { get; set; }

        [JsonPropertyName("examples")]
        public List<string>? Examples { get; set; }

        [JsonPropertyName("interactive")]
        public bool? Interactive { get; set; }

        [JsonPropertyName("metadata")]
        public List<MetadataJson>? Metadata { get; set; }
    }

    public sealed class ArgumentJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("arity")]
        public ArityJson? Arity { get; set; }

        [JsonPropertyName("acceptedValues")]
        public List<string>? AcceptedValues { get; set; }

        [JsonPropertyName("group")]
        public string? Group { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("hidden")]
        public bool? Hidden { get; set; }

        [JsonPropertyName("metadata")]
        public List<MetadataJson>? Metadata { get; set; }
    }

    public sealed class OptionJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("aliases")]
        public List<string>? Aliases { get; set; }

        [JsonPropertyName("arguments")]
        public List<ArgumentJson>? Arguments { get; set; }

        [JsonPropertyName("group")]
        public string? Group { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("recursive")]
        public bool? Recursive { get; set; }

        [JsonPropertyName("hidden")]
        public bool? Hidden { get; set; }

        [JsonPropertyName("metadata")]
        public List<MetadataJson>? Metadata { get; set; }
    }

    public sealed class ExitCodeJson
    {
        [JsonPropertyName("code")]
        public int? Code { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public sealed class MetadataJson
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public object? Value { get; set; }
    }

    public sealed class ArityJson
    {
        [JsonPropertyName("minimum")]
        public int? Minimum { get; set; }

        [JsonPropertyName("maximum")]
        public int? Maximum { get; set; }
    }
}