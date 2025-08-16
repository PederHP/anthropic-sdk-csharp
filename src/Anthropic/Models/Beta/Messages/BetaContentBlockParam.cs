using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Beta.Messages.BetaContentBlockParamVariants;

namespace Anthropic.Models.Beta.Messages;

/// <summary>
/// Regular text content.
/// </summary>
[JsonConverter(typeof(BetaContentBlockParamConverter))]
public abstract record class BetaContentBlockParam
{
    internal BetaContentBlockParam() { }

    public static implicit operator BetaContentBlockParam(BetaTextBlockParam value) =>
        new BetaTextBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaImageBlockParam value) =>
        new BetaImageBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaRequestDocumentBlock value) =>
        new BetaRequestDocumentBlockVariant(value);

    public static implicit operator BetaContentBlockParam(BetaSearchResultBlockParam value) =>
        new BetaSearchResultBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaThinkingBlockParam value) =>
        new BetaThinkingBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaRedactedThinkingBlockParam value) =>
        new BetaRedactedThinkingBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaToolUseBlockParam value) =>
        new BetaToolUseBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaToolResultBlockParam value) =>
        new BetaToolResultBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaServerToolUseBlockParam value) =>
        new BetaServerToolUseBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(
        BetaWebSearchToolResultBlockParam value
    ) => new BetaWebSearchToolResultBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(
        BetaCodeExecutionToolResultBlockParam value
    ) => new BetaCodeExecutionToolResultBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaMCPToolUseBlockParam value) =>
        new BetaMCPToolUseBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(
        BetaRequestMCPToolResultBlockParam value
    ) => new BetaRequestMCPToolResultBlockParamVariant(value);

    public static implicit operator BetaContentBlockParam(BetaContainerUploadBlockParam value) =>
        new BetaContainerUploadBlockParamVariant(value);

    public abstract void Validate();
}

sealed class BetaContentBlockParamConverter : JsonConverter<BetaContentBlockParam>
{
    public override BetaContentBlockParam? Read(
        ref Utf8JsonReader reader,
        Type _typeToConvert,
        JsonSerializerOptions options
    )
    {
        var json = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        string? type;
        try
        {
            type = json.GetProperty("type").GetString();
        }
        catch
        {
            type = null;
        }

        switch (type)
        {
            case "text":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaTextBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaTextBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "image":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaImageBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaImageBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "document":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaRequestDocumentBlock>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaRequestDocumentBlockVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "search_result":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaSearchResultBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaSearchResultBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "thinking":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaThinkingBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaThinkingBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "redacted_thinking":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaRedactedThinkingBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaRedactedThinkingBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "tool_use":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolUseBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaToolUseBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "tool_result":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolResultBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaToolResultBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "server_tool_use":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaServerToolUseBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaServerToolUseBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "web_search_tool_result":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized =
                        JsonSerializer.Deserialize<BetaWebSearchToolResultBlockParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new BetaWebSearchToolResultBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "code_execution_tool_result":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized =
                        JsonSerializer.Deserialize<BetaCodeExecutionToolResultBlockParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new BetaCodeExecutionToolResultBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "mcp_tool_use":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaMCPToolUseBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaMCPToolUseBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "mcp_tool_result":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized =
                        JsonSerializer.Deserialize<BetaRequestMCPToolResultBlockParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new BetaRequestMCPToolResultBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "container_upload":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaContainerUploadBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaContainerUploadBlockParamVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            default:
            {
                throw new Exception();
            }
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        BetaContentBlockParam value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            BetaTextBlockParamVariant(var betaTextBlockParam) => betaTextBlockParam,
            BetaImageBlockParamVariant(var betaImageBlockParam) => betaImageBlockParam,
            BetaRequestDocumentBlockVariant(var betaRequestDocumentBlock) =>
                betaRequestDocumentBlock,
            BetaSearchResultBlockParamVariant(var betaSearchResultBlockParam) =>
                betaSearchResultBlockParam,
            BetaThinkingBlockParamVariant(var betaThinkingBlockParam) => betaThinkingBlockParam,
            BetaRedactedThinkingBlockParamVariant(var betaRedactedThinkingBlockParam) =>
                betaRedactedThinkingBlockParam,
            BetaToolUseBlockParamVariant(var betaToolUseBlockParam) => betaToolUseBlockParam,
            BetaToolResultBlockParamVariant(var betaToolResultBlockParam) =>
                betaToolResultBlockParam,
            BetaServerToolUseBlockParamVariant(var betaServerToolUseBlockParam) =>
                betaServerToolUseBlockParam,
            BetaWebSearchToolResultBlockParamVariant(var betaWebSearchToolResultBlockParam) =>
                betaWebSearchToolResultBlockParam,
            BetaCodeExecutionToolResultBlockParamVariant(
                var betaCodeExecutionToolResultBlockParam
            ) => betaCodeExecutionToolResultBlockParam,
            BetaMCPToolUseBlockParamVariant(var betaMCPToolUseBlockParam) =>
                betaMCPToolUseBlockParam,
            BetaRequestMCPToolResultBlockParamVariant(var betaRequestMCPToolResultBlockParam) =>
                betaRequestMCPToolResultBlockParam,
            BetaContainerUploadBlockParamVariant(var betaContainerUploadBlockParam) =>
                betaContainerUploadBlockParam,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
