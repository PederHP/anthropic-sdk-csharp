using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Beta.Messages.BetaToolChoiceVariants;

namespace Anthropic.Models.Beta.Messages;

/// <summary>
/// How the model should use the provided tools. The model can use a specific tool,
/// any available tool, decide by itself, or not use tools at all.
/// </summary>
[JsonConverter(typeof(BetaToolChoiceConverter))]
public abstract record class BetaToolChoice
{
    internal BetaToolChoice() { }

    public static implicit operator BetaToolChoice(BetaToolChoiceAuto value) =>
        new BetaToolChoiceAutoVariant(value);

    public static implicit operator BetaToolChoice(BetaToolChoiceAny value) =>
        new BetaToolChoiceAnyVariant(value);

    public static implicit operator BetaToolChoice(BetaToolChoiceTool value) =>
        new BetaToolChoiceToolVariant(value);

    public static implicit operator BetaToolChoice(BetaToolChoiceNone value) =>
        new BetaToolChoiceNoneVariant(value);

    public abstract void Validate();
}

sealed class BetaToolChoiceConverter : JsonConverter<BetaToolChoice>
{
    public override BetaToolChoice? Read(
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
            case "auto":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolChoiceAuto>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaToolChoiceAutoVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "any":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolChoiceAny>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaToolChoiceAnyVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "tool":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolChoiceTool>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaToolChoiceToolVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "none":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolChoiceNone>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaToolChoiceNoneVariant(deserialized);
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
        BetaToolChoice value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            BetaToolChoiceAutoVariant(var betaToolChoiceAuto) => betaToolChoiceAuto,
            BetaToolChoiceAnyVariant(var betaToolChoiceAny) => betaToolChoiceAny,
            BetaToolChoiceToolVariant(var betaToolChoiceTool) => betaToolChoiceTool,
            BetaToolChoiceNoneVariant(var betaToolChoiceNone) => betaToolChoiceNone,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
