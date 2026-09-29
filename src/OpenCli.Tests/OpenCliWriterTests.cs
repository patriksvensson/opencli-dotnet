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
            Command = new OpenCliCommand
            {
                Name = "test",
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
            },
            Info = new OpenCliInfo
            {
                Title = "A test appliation",
                Version = "1.0",
            },
        };

        // When
        var result = document.Write();

        // Then
        result.ShouldBe(
            """
            {
              "opencli": "draft",
              "command": {
                "name": "test",
                "arguments": [
                  {
                    "name": "VALUE",
                    "required": true,
                    "arity": {
                      "minimum": 1
                    }
                  }
                ]
              },
              "info": {
                "title": "A test appliation",
                "version": "1.0"
              }
            }
            """);
    }

    [Fact]
    public async Task Should_Write_Parsed_Description_Unchanged()
    {
        // Given
        const string json =
            """
            {
              "opencli": "0.1",
              "command": {
                "name": "git",
                "aliases": [
                  "g"
                ],
                "options": [
                  {
                    "name": "--config",
                    "required": false,
                    "aliases": [
                      "-c"
                    ],
                    "arguments": [
                      {
                        "name": "NAME=VALUE",
                        "required": true,
                        "arity": {
                          "minimum": 1,
                          "maximum": 1
                        }
                      }
                    ],
                    "group": "Global",
                    "description": "Pass a configuration parameter to the command.",
                    "recursive": true,
                    "hidden": false,
                    "metadata": [
                      {
                        "name": "string",
                        "value": "text"
                      },
                      {
                        "name": "number",
                        "value": 42
                      },
                      {
                        "name": "boolean",
                        "value": true
                      },
                      {
                        "name": "array",
                        "value": [
                          "a",
                          "b"
                        ]
                      },
                      {
                        "name": "object",
                        "value": {
                          "key": "value"
                        }
                      }
                    ]
                  }
                ],
                "arguments": [
                  {
                    "name": "PATHSPEC",
                    "required": false,
                    "arity": {
                      "minimum": 0
                    },
                    "acceptedValues": [
                      "foo",
                      "bar"
                    ],
                    "group": "Paths",
                    "description": "Limit the command to the given paths.",
                    "hidden": true,
                    "metadata": [
                      {
                        "name": "ClrType",
                        "value": "System.String[]"
                      }
                    ]
                  }
                ],
                "commands": [
                  {
                    "name": "clone",
                    "description": "Clone a repository into a new directory.",
                    "examples": [
                      "git clone https://github.com/spectreconsole/open-cli.git"
                    ],
                    "interactive": true
                  }
                ],
                "exitCodes": [
                  {
                    "code": 0,
                    "description": "Success"
                  },
                  {
                    "code": 128,
                    "description": "Fatal error"
                  }
                ],
                "description": "The stupid content tracker",
                "hidden": false,
                "examples": [
                  "git --version"
                ],
                "interactive": false,
                "metadata": [
                  {
                    "name": "generator",
                    "value": "handwritten"
                  }
                ]
              },
              "info": {
                "title": "git",
                "summary": "The stupid content tracker",
                "description": "Git is a fast, scalable, distributed revision control system.",
                "contact": {
                  "name": "Git",
                  "url": "https://git-scm.com",
                  "email": "git@vger.kernel.org"
                },
                "license": {
                  "name": "GNU General Public License v2.0 only",
                  "identifier": "GPL-2.0-only",
                  "url": "https://opensource.org/license/gpl-2-0"
                },
                "version": "2.50.1"
              },
              "conventions": {
                "groupOptions": true,
                "optionSeparator": " "
              }
            }
            """;

        // When
        var result = await OpenCliDocument.Parse(json);

        // Then
        result.Document.ShouldNotBeNull().Write().ShouldBe(json);
    }
}
