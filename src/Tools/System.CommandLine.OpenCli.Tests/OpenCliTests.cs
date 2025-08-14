using System.CommandLine.OpenCli.Tests.Fixtures;

namespace System.CommandLine.OpenCli.Tests;

public sealed class OpenCliTests
{
    [Fact]
    [Expectation("Generated")]
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

            root.Subcommands.Add(greetCommand);
        });

        // When
        var (exitCode, output) = fixture.Run(
            settings: new OpenCliSettings
            {
                Title = "MyApp",
                Version = "1.2.3",
            });

        // Then
        await Verify(output, extension: "json");
    }
}