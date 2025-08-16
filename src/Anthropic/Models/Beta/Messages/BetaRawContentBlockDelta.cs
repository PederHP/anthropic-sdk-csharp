using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Beta.Messages.BetaRawContentBlockDeltaVariants;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(typeof(BetaRawContentBlockDeltaConverter))]
public abstract record class BetaRawContentBlockDelta
{
    internal BetaRawContentBlockDelta() { }

    public static implicit operator BetaRawContentBlockDelta(BetaTextDelta value) =>
        new BetaTextDeltaVariant(value);

    public static implicit operator BetaRawContentBlockDelta(BetaInputJSONDelta value) =>
        new BetaInputJSONDeltaVariant(value);

    public static implicit operator BetaRawContentBlockDelta(BetaCitationsDelta value) =>
        new BetaCitationsDeltaVariant(value);

    public static implicit operator BetaRawContentBlockDelta(BetaThinkingDelta value) =>
        new BetaThinkingDeltaVariant(value);

    public static implicit operator BetaRawContentBlockDelta(BetaSignatureDelta value) =>
        new BetaSignatureDeltaVariant(value);

    public abstract void Validate();
}

sealed class BetaRawContentBlockDeltaConverter : JsonConverter<BetaRawContentBlockDelta>
{
    public override BetaRawContentBlockDelta? Read(
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
            case "text_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaTextDelta>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaTextDeltaVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "input_json_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaInputJSONDelta>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaInputJSONDeltaVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "citations_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaCitationsDelta>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaCitationsDeltaVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "thinking_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaThinkingDelta>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaThinkingDeltaVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "signature_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaSignatureDelta>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaSignatureDeltaVariant(deserialized);
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
        BetaRawContentBlockDelta value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            BetaTextDeltaVariant(var betaTextDelta) => betaTextDelta,
            BetaInputJSONDeltaVariant(var betaInputJSONDelta) => betaInputJSONDelta,
            BetaCitationsDeltaVariant(var betaCitationsDelta) => betaCitationsDelta,
            BetaThinkingDeltaVariant(var betaThinkingDelta) => betaThinkingDelta,
            BetaSignatureDeltaVariant(var betaSignatureDelta) => betaSignatureDelta,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
