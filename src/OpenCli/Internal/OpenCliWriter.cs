using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenCli.Internal;

internal static class OpenCliWriter
{
    private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static string Write(OpenCliDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        return JsonSerializer.Serialize(
            OpenCliMapper.Map(document),
            _options);
    }

    public static void Write(Stream stream, OpenCliDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        JsonSerializer.Serialize(
            stream,
            OpenCliMapper.Map(document),
            _options);
    }
}