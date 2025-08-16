using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.MessageCountTokensToolVariants;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(MessageCountTokensToolConverter))]
public abstract record class MessageCountTokensTool
{
    internal MessageCountTokensTool() { }

    public static implicit operator MessageCountTokensTool(Tool value) => new ToolVariant(value);

    public static implicit operator MessageCountTokensTool(ToolBash20250124 value) =>
        new ToolBash20250124Variant(value);

    public static implicit operator MessageCountTokensTool(ToolTextEditor20250124 value) =>
        new ToolTextEditor20250124Variant(value);

    public static implicit operator MessageCountTokensTool(ToolTextEditor20250429 value) =>
        new ToolTextEditor20250429Variant(value);

    public static implicit operator MessageCountTokensTool(ToolTextEditor20250728 value) =>
        new ToolTextEditor20250728Variant(value);

    public static implicit operator MessageCountTokensTool(WebSearchTool20250305 value) =>
        new WebSearchTool20250305Variant(value);

    public abstract void Validate();
}

sealed class MessageCountTokensToolConverter : JsonConverter<MessageCountTokensTool>
{
    public override MessageCountTokensTool? Read(
        ref Utf8JsonReader reader,
        Type _typeToConvert,
        JsonSerializerOptions options
    )
    {
        List<JsonException> exceptions = [];

        try
        {
            var deserialized = JsonSerializer.Deserialize<Tool>(ref reader, options);
            if (deserialized != null)
            {
                return new ToolVariant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<ToolBash20250124>(ref reader, options);
            if (deserialized != null)
            {
                return new ToolBash20250124Variant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<ToolTextEditor20250124>(
                ref reader,
                options
            );
            if (deserialized != null)
            {
                return new ToolTextEditor20250124Variant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<ToolTextEditor20250429>(
                ref reader,
                options
            );
            if (deserialized != null)
            {
                return new ToolTextEditor20250429Variant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<ToolTextEditor20250728>(
                ref reader,
                options
            );
            if (deserialized != null)
            {
                return new ToolTextEditor20250728Variant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<WebSearchTool20250305>(
                ref reader,
                options
            );
            if (deserialized != null)
            {
                return new WebSearchTool20250305Variant(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        throw new AggregateException(exceptions);
    }

    public override void Write(
        Utf8JsonWriter writer,
        MessageCountTokensTool value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            ToolVariant(var tool) => tool,
            ToolBash20250124Variant(var toolBash20250124) => toolBash20250124,
            ToolTextEditor20250124Variant(var toolTextEditor20250124) => toolTextEditor20250124,
            ToolTextEditor20250429Variant(var toolTextEditor20250429) => toolTextEditor20250429,
            ToolTextEditor20250728Variant(var toolTextEditor20250728) => toolTextEditor20250728,
            WebSearchTool20250305Variant(var webSearchTool20250305) => webSearchTool20250305,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
