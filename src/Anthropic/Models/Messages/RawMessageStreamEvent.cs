using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.RawMessageStreamEventVariants;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(RawMessageStreamEventConverter))]
public abstract record class RawMessageStreamEvent
{
    internal RawMessageStreamEvent() { }

    public static implicit operator RawMessageStreamEvent(RawMessageStartEvent value) =>
        new RawMessageStartEventVariant(value);

    public static implicit operator RawMessageStreamEvent(RawMessageDeltaEvent value) =>
        new RawMessageDeltaEventVariant(value);

    public static implicit operator RawMessageStreamEvent(RawMessageStopEvent value) =>
        new RawMessageStopEventVariant(value);

    public static implicit operator RawMessageStreamEvent(RawContentBlockStartEvent value) =>
        new RawContentBlockStartEventVariant(value);

    public static implicit operator RawMessageStreamEvent(RawContentBlockDeltaEvent value) =>
        new RawContentBlockDeltaEventVariant(value);

    public static implicit operator RawMessageStreamEvent(RawContentBlockStopEvent value) =>
        new RawContentBlockStopEventVariant(value);

    public abstract void Validate();
}

sealed class RawMessageStreamEventConverter : JsonConverter<RawMessageStreamEvent>
{
    public override RawMessageStreamEvent? Read(
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
            case "message_start":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawMessageStartEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawMessageStartEventVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "message_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawMessageDeltaEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawMessageDeltaEventVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "message_stop":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawMessageStopEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawMessageStopEventVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "content_block_start":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawContentBlockStartEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawContentBlockStartEventVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "content_block_delta":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawContentBlockDeltaEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawContentBlockDeltaEventVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "content_block_stop":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<RawContentBlockStopEvent>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new RawContentBlockStopEventVariant(deserialized);
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
        RawMessageStreamEvent value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            RawMessageStartEventVariant(var rawMessageStartEvent) => rawMessageStartEvent,
            RawMessageDeltaEventVariant(var rawMessageDeltaEvent) => rawMessageDeltaEvent,
            RawMessageStopEventVariant(var rawMessageStopEvent) => rawMessageStopEvent,
            RawContentBlockStartEventVariant(var rawContentBlockStartEvent) =>
                rawContentBlockStartEvent,
            RawContentBlockDeltaEventVariant(var rawContentBlockDeltaEvent) =>
                rawContentBlockDeltaEvent,
            RawContentBlockStopEventVariant(var rawContentBlockStopEvent) =>
                rawContentBlockStopEvent,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
