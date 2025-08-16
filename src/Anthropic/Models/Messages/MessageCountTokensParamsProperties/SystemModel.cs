using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using SystemVariants = Anthropic.Models.Messages.MessageCountTokensParamsProperties.SystemVariants;

namespace Anthropic.Models.Messages.MessageCountTokensParamsProperties;

/// <summary>
/// System prompt.
///
/// A system prompt is a way of providing context and instructions to Claude, such
/// as specifying a particular goal or role. See our [guide to system prompts](https://docs.anthropic.com/en/docs/system-prompts).
/// </summary>
[JsonConverter(typeof(SystemModelConverter))]
public abstract record class SystemModel
{
    internal SystemModel() { }

    public static implicit operator SystemModel(string value) => new SystemVariants::String(value);

    public static implicit operator SystemModel(List<TextBlockParam> value) =>
        new SystemVariants::TextBlockParams(value);

    public abstract void Validate();
}

sealed class SystemModelConverter : JsonConverter<SystemModel>
{
    public override SystemModel? Read(
        ref Utf8JsonReader reader,
        Type _typeToConvert,
        JsonSerializerOptions options
    )
    {
        List<JsonException> exceptions = [];

        try
        {
            var deserialized = JsonSerializer.Deserialize<string>(ref reader, options);
            if (deserialized != null)
            {
                return new SystemVariants::String(deserialized);
            }
        }
        catch (JsonException e)
        {
            exceptions.Add(e);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<TextBlockParam>>(
                ref reader,
                options
            );
            if (deserialized != null)
            {
                return new SystemVariants::TextBlockParams(deserialized);
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
        SystemModel value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            SystemVariants::String(var string1) => string1,
            SystemVariants::TextBlockParams(var textBlockParams) => textBlockParams,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
