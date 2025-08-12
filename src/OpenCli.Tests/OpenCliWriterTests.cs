namespace OpenCli.Tests;

public sealed class OpenCliWriterTests
{
    [Fact]
    public void Write()
    {
        // Given
        var document = new OpenCliDocument
        {
            OpenCli = "draft",
            Info = new OpenCliInfo
            {
                Title = "A test appliation",
                Version = "1.0",
            },
            Arguments =
            [
                new OpenCliArgument
                {
                    Name = "VALUE",
                    Required = true,
                    Arity = new OpenCliArity
                    {
                        Minimum = 1,
                    },
                },

            ],
        };

        // When
        var result = document.Write();

        // Then
        result.ShouldBe(
            """
            {
              "opencli": "draft",
              "info": {
                "title": "A test appliation",
                "version": "1.0"
              },
              "arguments": [
                {
                  "name": "VALUE",
                  "required": true,
                  "arity": {
                    "minimum": 1
                  }
                }
              ]
            }
            """);
    }
}