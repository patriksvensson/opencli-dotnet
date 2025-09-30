#if OPENCLI
#pragma warning restore
#else
#pragma warning disable
#endif

using System.Text.Json;
using NJsonSchema;
using NJsonSchema.Validation;
using OpenCli.Diagnostics;
using OpenCli.Internal;

namespace OpenCli;

internal static class OpenCliParser
{
    private const string Schema =
        """
        {
            "$schema": "https://json-schema.org/draft/2020-12/schema",
            "$id": "OpenCLI.json",
            "type": "object",
            "properties": {
                "opencli": {
                    "type": "string",
                    "description": "The OpenCLI version number"
                },
                "info": {
                    "$ref": "#/definitions/CliInfo",
                    "description": "Information about the CLI"
                },
                "conventions": {
                    "$ref": "#/definitions/Conventions",
                    "description": "The conventions used by the CLI"
                },
                "arguments": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/Argument"
                    },
                    "description": "Root command arguments"
                },
                "options": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/Option"
                    },
                    "description": "Root command options"
                },
                "commands": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/Command"
                    },
                    "description": "Root command sub commands"
                },
                "exitCodes": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/ExitCode"
                    },
                    "description": "Root command exit codes"
                },
                "examples": {
                    "type": "array",
                    "items": {
                        "type": "string"
                    },
                    "description": "Examples of how to use the CLI"
                },
                "interactive": {
                    "type": "boolean",
                    "description": "Indicates whether or not the command requires interactive input"
                },
                "metadata": {
                    "type": "array",
                    "items": {
                        "$ref": "#/definitions/Metadata"
                    },
                    "description": "Custom metadata"
                }
            },
            "required": [
                "opencli",
                "info"
            ],
            "description": "The OpenCLI description",
            "definitions": {
                "CliInfo": {
                    "type": "object",
                    "properties": {
                        "title": {
                            "type": "string",
                            "description": "The application title"
                        },
                        "summary": {
                            "type": "string",
                            "description": "A short summary of the application"
                        },
                        "description": {
                            "type": "string",
                            "description": "A description of the application"
                        },
                        "contact": {
                            "$ref": "#/definitions/Contact",
                            "description": "The contact information"
                        },
                        "license": {
                            "$ref": "#/definitions/License",
                            "description": "The application license"
                        },
                        "version": {
                            "type": "string",
                            "description": "The application version"
                        }
                    },
                    "required": [
                        "title",
                        "version"
                    ]
                },
                "Conventions": {
                    "type": "object",
                    "properties": {
                        "groupOptions": {
                            "type": "boolean",
                            "default": true,
                            "description": "Whether or not grouping of short options are allowed"
                        },
                        "optionSeparator": {
                            "type": "string",
                            "default": " ",
                            "description": "The option argument separator"
                        }
                    }
                },
                "Argument": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string",
                            "description": "The argument name"
                        },
                        "required": {
                            "type": "boolean",
                            "description": "Whether or not the argument is required"
                        },
                        "arity": {
                            "$ref": "#/definitions/Arity",
                            "description": "The argument arity. Arity defines the minimum and maximum number of argument values"
                        },
                        "acceptedValues": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            },
                            "description": "A list of accepted values"
                        },
                        "group": {
                            "type": "string",
                            "description": "The argument group"
                        },
                        "description": {
                            "type": "string",
                            "description": "The argument description"
                        },
                        "hidden": {
                            "type": "boolean",
                            "default": false,
                            "description": "Whether or not the argument is hidden"
                        },
                        "metadata": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Metadata"
                            },
                            "description": "Custom metadata"
                        }
                    },
                    "required": [
                        "name"
                    ]
                },
                "Option": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string",
                            "description": "The option name"
                        },
                        "required": {
                            "type": "boolean",
                            "description": "Whether or not the option is required"
                        },
                        "aliases": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            },
                            "uniqueItems": true,
                            "description": "The option's aliases"
                        },
                        "arguments": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Argument"
                            },
                            "description": "The option's arguments"
                        },
                        "group": {
                            "type": "string",
                            "description": "The option group"
                        },
                        "description": {
                            "type": "string",
                            "description": "The option description"
                        },
                        "recursive": {
                            "type": "boolean",
                            "default": false,
                            "description": "Specifies whether the option is accessible from the immediate parent command and, recursively, from its subcommands"
                        },
                        "hidden": {
                            "type": "boolean",
                            "default": false,
                            "description": "Whether or not the option is hidden"
                        },
                        "metadata": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Metadata"
                            },
                            "description": "Custom metadata"
                        }
                    },
                    "required": [
                        "name"
                    ]
                },
                "Command": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string",
                            "description": "The command name"
                        },
                        "aliases": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            },
                            "uniqueItems": true,
                            "description": "The command aliases"
                        },
                        "options": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Option"
                            },
                            "description": "The command options"
                        },
                        "arguments": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Argument"
                            },
                            "description": "The command arguments"
                        },
                        "commands": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Command"
                            },
                            "description": "The command's sub commands"
                        },
                        "exitCodes": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/ExitCode"
                            },
                            "description": "The command's exit codes"
                        },
                        "description": {
                            "type": "string",
                            "description": "The command description"
                        },
                        "hidden": {
                            "type": "boolean",
                            "default": false,
                            "description": "Whether or not the command is hidden"
                        },
                        "examples": {
                            "type": "array",
                            "items": {
                                "type": "string"
                            },
                            "description": "Examples of how to use the command"
                        },
                        "interactive": {
                            "type": "boolean",
                            "description": "Indicate whether or not the command requires interactive input"
                        },
                        "metadata": {
                            "type": "array",
                            "items": {
                                "$ref": "#/definitions/Metadata"
                            },
                            "description": "Custom metadata"
                        }
                    },
                    "required": [
                        "name"
                    ]
                },
                "ExitCode": {
                    "type": "object",
                    "properties": {
                        "code": {
                            "type": "integer",
                            "description": "The exit code"
                        },
                        "description": {
                            "type": "string",
                            "description": "The exit code description"
                        }
                    },
                    "required": [
                        "code"
                    ]
                },
                "Metadata": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string"
                        },
                        "value": {}
                    },
                    "required": [
                        "name"
                    ]
                },
                "Contact": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string",
                            "description": "The identifying name of the contact person/organization"
                        },
                        "url": {
                            "type": "string",
                            "format": "uri",
                            "description": "The URI for the contact information. This MUST be in the form of a URI."
                        },
                        "email": {
                            "$ref": "#/definitions/email",
                            "description": "The email address of the contact person/organization. This MUST be in the form of an email address."
                        }
                    },
                    "description": "Contact information"
                },
                "License": {
                    "type": "object",
                    "properties": {
                        "name": {
                            "type": "string",
                            "description": "The license name"
                        },
                        "identifier": {
                            "type": "string",
                            "description": "The SPDX license identifier"
                        }
                    }
                },
                "Arity": {
                    "type": "object",
                    "properties": {
                        "minimum": {
                            "type": "integer",
                            "minimum": 0,
                            "description": "The minimum number of values allowed"
                        },
                        "maximum": {
                            "type": "integer",
                            "minimum": 0,
                            "description": "The maximum number of values allowed"
                        }
                    },
                    "description": "Arity defines the minimum and maximum number of argument values"
                },
                "email": {
                    "type": "string",
                    "pattern": ".+\\@.+\\..+"
                }
            }
        }
        """;

    public static async Task<OpenCliParseResult> Parse(
        string json,
        CancellationToken cancellationToken = default)
    {
        await using var stream = json.ToStream();
        return await Parse(stream, cancellationToken);
    }

    public static async Task<OpenCliParseResult> Parse(
        Stream stream,
        CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync(cancellationToken);

        var diagnostics = new OpenCliDiagnosticsCollection();

        // Validate the schema
        var validationResult = await ValidateSchema(json, cancellationToken);
        if (validationResult != null)
        {
            foreach (var error in validationResult)
            {
                if (error.HasLineInfo)
                {
                    diagnostics.Add(
                        new OpenCliLocation
                        {
                            Row = error.LineNumber,
                            Column = error.LinePosition,
                        },
                        ParseErrors.SchemaError(error.ToString()));
                }
                else
                {
                    diagnostics.Add(
                        ParseErrors.SchemaError(error.ToString()));
                }
            }
        }
        else
        {
            diagnostics.Add(
                ParseErrors.InvalidJson());
        }

        // If there are errors, abort parsing. There might be problems
        // with parsing JSON or other problems.
        if (diagnostics.HasErrors)
        {
            return new OpenCliParseResult
            {
                Document = null,
                Diagnostics = diagnostics,
            };
        }

        // Parse the JSON
        var jsonDocument = ParseJsonModel(json, diagnostics);
        if (jsonDocument == null)
        {
            return new OpenCliParseResult
            {
                Document = null,
                Diagnostics = diagnostics,
            };
        }

        // Map the JSON document
        var document = JsonMapper.Map(jsonDocument);

        return new OpenCliParseResult
        {
            Document = document,
            Diagnostics = diagnostics,
        };
    }

    private static async Task<ICollection<ValidationError>?> ValidateSchema(
        string json,
        CancellationToken cancellationToken)
    {
        try
        {
            var schema = await JsonSchema.FromJsonAsync(Schema, cancellationToken);
            return schema.Validate(json);
        }
        catch
        {
            return null;
        }
    }

    private static JsonModel.DocumentJson? ParseJsonModel(string json, OpenCliDiagnosticsCollection diagnostics)
    {
        try
        {
#pragma warning disable IL2026
#pragma warning disable IL3050
            // TODO: Remove pragma
            var jsonDocument = JsonSerializer.Deserialize<JsonModel.DocumentJson>(
                json, new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                });
#pragma warning restore IL3050
#pragma warning restore IL2026
            return jsonDocument;
        }
        catch
        {
            diagnostics.Add(
                ParseErrors.InvalidJson());

            return null;
        }
    }
}