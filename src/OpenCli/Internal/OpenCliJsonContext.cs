#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using System.Text.Json.Serialization;

namespace OpenCli.Internal;

[JsonSerializable(typeof(JsonModel.DocumentJson))]
[JsonSourceGenerationOptions(
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    AllowTrailingCommas = true)]
internal partial class OpenCliJsonContext : JsonSerializerContext
{
}
