using Shouldly;

namespace Jinn.OpenCli.Tests;

public class OpenCliTests
{
    [Fact]
    public async Task Should_Generate_OpenCli()
    {
        // Given
        var fixture = new OpenCliFixture(root =>
        {
            root.Arguments.Add(new Argument<int>("COUNT")
            {
                Description = "The number of greetings",
                Hidden = false,
            });

            var greetCommand = new Command("greet");
            greetCommand.Arguments.Add(new Argument<string>("NAME")
            {
                Description = "The name of the person to greet",
            });

            greetCommand.Options.Add(new Option<int>("--age")
            {
                Description = "The age of the person to greet",
            });

            root.Commands.Add(greetCommand);
        });

        // When
        var (exitCode, output) = await fixture.Run(
            settings: new OpenCliSettings
            {
                Title = "MyApp",
                Version = "1.2.3",
            });

        // Then
        await Verify(output, extension: "json");
    }
}