using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.ContentBlockParamVariants;

namespace Anthropic.Models.Messages;

/// <summary>
/// Regular text content.
/// </summary>
[JsonConverter(typeof(ContentBlockParamConverter))]
public abstract record class ContentBlockParam
{
    internal ContentBlockParam() { }

    public static implicit operator ContentBlockParam(TextBlockParam value) =>
        new TextBlockParamVariant(value);

    public static implicit operator ContentBlockParam(ImageBlockParam value) =>
        new ImageBlockParamVariant(value);

    public static implicit operator ContentBlockParam(DocumentBlockParam value) =>
        new DocumentBlockParamVariant(value);

    public static implicit operator ContentBlockParam(SearchResultBlockParam value) =>
        new SearchResultBlockParamVariant(value);

    public static implicit operator ContentBlockParam(ThinkingBlockParam value) =>
        new ThinkingBlockParamVariant(value);

    public static implicit operator ContentBlockParam(RedactedThinkingBlockParam value) =>
        new RedactedThinkingBlockParamVariant(value);

    public static implicit operator ContentBlockParam(ToolUseBlockParam value) =>
        new ToolUseBlockParamVariant(value);

    public static implicit operator ContentBlockParam(ToolResultBlockParam value) =>
        new ToolResultBlockParamVariant(value);

    public static implicit operator ContentBlockParam(ServerToolUseBlockParam value) =>
        new ServerToolUseBlockParamVariant(value);

    public static implicit operator ContentBlockParam(WebSearchToolResultBlockParam value) =>
        new WebSearchToolResultBlockParamVariant(value);

    public abstract void Validate();
}

sealed class ContentBlockParamConverter : JsonConverter<ContentBlockParam>
{
    public override ContentBlockParam? Read(
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
                    var deserialized = JsonSerializer.Deserialize<TextBlockParam>(json, options);
                    if (deserialized != null)
                    {
                        return new TextBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<ImageBlockParam>(json, options);
                    if (deserialized != null)
                    {
                        return new ImageBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<DocumentBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new DocumentBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<SearchResultBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new SearchResultBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<ThinkingBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new ThinkingBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<RedactedThinkingBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RedactedThinkingBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<ToolUseBlockParam>(json, options);
                    if (deserialized != null)
                    {
                        return new ToolUseBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<ToolResultBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new ToolResultBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<ServerToolUseBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new ServerToolUseBlockParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<WebSearchToolResultBlockParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new WebSearchToolResultBlockParamVariant(deserialized);
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
        ContentBlockParam value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            TextBlockParamVariant(var textBlockParam) => textBlockParam,
            ImageBlockParamVariant(var imageBlockParam) => imageBlockParam,
            DocumentBlockParamVariant(var documentBlockParam) => documentBlockParam,
            SearchResultBlockParamVariant(var searchResultBlockParam) => searchResultBlockParam,
            ThinkingBlockParamVariant(var thinkingBlockParam) => thinkingBlockParam,
            RedactedThinkingBlockParamVariant(var redactedThinkingBlockParam) =>
                redactedThinkingBlockParam,
            ToolUseBlockParamVariant(var toolUseBlockParam) => toolUseBlockParam,
            ToolResultBlockParamVariant(var toolResultBlockParam) => toolResultBlockParam,
            ServerToolUseBlockParamVariant(var serverToolUseBlockParam) => serverToolUseBlockParam,
            WebSearchToolResultBlockParamVariant(var webSearchToolResultBlockParam) =>
                webSearchToolResultBlockParam,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
