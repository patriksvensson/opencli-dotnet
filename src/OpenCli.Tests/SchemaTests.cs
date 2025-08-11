namespace OpenCli.Tests;

public class SchemaTests
{
    [Fact]
    public async Task Should_Create_Error_Diagnostics_For_Invalid_Json()
    {
        // Given, When
        var result = await OpenCliParser.Parse("This is not valid JSON");

        // Then
        result.Diagnostics.HasErrors.ShouldBeTrue();
        result.Diagnostics[0].Code.ShouldBe("OPENCLI0001");
    }

    [Fact]
    public async Task Should_Create_Error_Diagnostics_For_Schema_Mismatch()
    {
        // Given, When
        var result = await OpenCliParser.Parse(
            """
            {
                "$schema": "../schema.json",
                "opencli": "0.1",
                "info": {
                    "version": "9.0.1",
                    "description": "The .NET CLI",
                    "license": {
                        "name": "MIT License",
                        "identifier": "MIT"
                    }
                },
                "options": [
                    {
                        "name": 3,
                        "aliases": [ "-h" ],
                        "description": "Display help."
                    },
                    {
                        "name": "--info",
                        "description": "Display .NET information."
                    },
                    {
                        "name": "--list-sdks",
                        "description": "Display the installed SDKs."
                    },
                    {
                        "name": "--list-runtimes",
                        "description": "Display the installed runtimes."
                    }
                ],
                "commands": [
                    {
                        "name": "build",
                        "arguments": [
                            {
                                "name": "PROJECT | SOLUTION",
                                "description": "The project or solution file to operate on. If a file is not specified, the command will search the current directory for one.",
                            }
                        ],
                        "options": [
                            {
                                "name": "--configuration",
                                "aliases": [ "-c" ],
                                "description": "The configuration to use for building the project. The default for most projects is 'Debug'.",
                                "arguments": [
                                    {
                                        "name": "CONFIGURATION",
                                        "required": true,
                                        "arity": {
                                            "minimum": 1,
                                            "maximum": 1
                                        }
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
            """);

        // Then
        result.HasErrors.ShouldBeTrue();
        result.Diagnostics.Count.ShouldBe(2);
    }

    [Fact]
    public async Task Should_Parse_Valid_Description()
    {
        // Given, When
        var result = await OpenCliParser.Parse(
            """
            {
                "$schema": "../schema.json",
                "opencli": "0.1",
                "info": {
                    "title": ".NET CLI",
                    "version": "9.0.1",
                    "description": "The .NET CLI",
                    "license": {
                        "name": "MIT License",
                        "identifier": "MIT"
                    }
                },
                "options": [
                    {
                        "name": "--help",
                        "aliases": [ "-h" ],
                        "description": "Display help."
                    },
                    {
                        "name": "--info",
                        "description": "Display .NET information."
                    },
                    {
                        "name": "--list-sdks",
                        "description": "Display the installed SDKs."
                    },
                    {
                        "name": "--list-runtimes",
                        "description": "Display the installed runtimes."
                    }
                ],
                "commands": [
                    {
                        "name": "build",
                        "arguments": [
                            {
                                "name": "PROJECT | SOLUTION",
                                "description": "The project or solution file to operate on. If a file is not specified, the command will search the current directory for one.",
                            }
                        ],
                        "options": [
                            {
                                "name": "--configuration",
                                "aliases": [ "-c" ],
                                "description": "The configuration to use for building the project. The default for most projects is 'Debug'.",
                                "arguments": [
                                    {
                                        "name": "CONFIGURATION",
                                        "required": true,
                                        "arity": {
                                            "minimum": 1,
                                            "maximum": 1
                                        }
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
            """);

        // Then
        result.HasErrors.ShouldBeFalse();
    }
}