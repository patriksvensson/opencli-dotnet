using Jinn.OpenCli;

namespace Jinn;

[PublicAPI]
public static class RootCommandExtensions
{
    extension(RootCommand command)
    {
        public void AddOpenCli(
            OpenCliSettings? settings = null,
            Action<InvocationContext, string>? writer = null)
        {
            command.Options.Add(new OpenCliOption(settings, writer));
        }
    }
}