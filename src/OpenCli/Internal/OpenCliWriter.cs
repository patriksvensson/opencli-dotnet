#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using System.Text.Json;

namespace OpenCli.Internal;

internal static class OpenCliWriter
{
    public static string Write(OpenCliDocument document)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return JsonSerializer.Serialize(
            OpenCliMapper.Map(document),
            OpenCliJsonContext.Default.DocumentJson);
    }

    public static void Write(Stream stream, OpenCliDocument document)
    {
        if (document == null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        JsonSerializer.Serialize(
            stream,
            OpenCliMapper.Map(document),
            OpenCliJsonContext.Default.DocumentJson);
    }
}