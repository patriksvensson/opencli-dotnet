namespace Jinn.OpenCli;

internal sealed class OpenCliOption : Option
{
    public OpenCliOption(
        OpenCliSettings? settings,
        Action<InvocationContext, string>? writer = null)
        : base("--opencli", new Argument<bool>("value"))
    {
        Handler = ctx =>
        {
            writer ??= (_, json) => Console.Write(json);
            writer(ctx, OpenCliGenerator.Generate(
                settings ?? new OpenCliSettings(),
                ctx.ParseResult));

            return Task.FromResult(false);
        };
    }
}