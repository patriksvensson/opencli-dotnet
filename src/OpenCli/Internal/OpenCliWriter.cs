using System.Text.Json;

namespace OpenCli.Internal;

internal static class OpenCliWriter
{
    public static string Write(OpenCliDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        return JsonSerializer.Serialize(OpenCliMapper.Map(document));
    }

    public static void Write(Stream stream, OpenCliDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);
        JsonSerializer.Serialize(stream, OpenCliMapper.Map(document));
    }
}