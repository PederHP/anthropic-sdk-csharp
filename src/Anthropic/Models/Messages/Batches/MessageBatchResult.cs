using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.Batches.MessageBatchResultVariants;

namespace Anthropic.Models.Messages.Batches;

/// <summary>
/// Processing result for this request.
///
/// Contains a Message output if processing was successful, an error response if processing
/// failed, or the reason why processing was not attempted, such as cancellation
/// or expiration.
/// </summary>
[JsonConverter(typeof(MessageBatchResultConverter))]
public abstract record class MessageBatchResult
{
    internal MessageBatchResult() { }

    public static implicit operator MessageBatchResult(MessageBatchSucceededResult value) =>
        new MessageBatchSucceededResultVariant(value);

    public static implicit operator MessageBatchResult(MessageBatchErroredResult value) =>
        new MessageBatchErroredResultVariant(value);

    public static implicit operator MessageBatchResult(MessageBatchCanceledResult value) =>
        new MessageBatchCanceledResultVariant(value);

    public static implicit operator MessageBatchResult(MessageBatchExpiredResult value) =>
        new MessageBatchExpiredResultVariant(value);

    public abstract void Validate();
}

sealed class MessageBatchResultConverter : JsonConverter<MessageBatchResult>
{
    public override MessageBatchResult? Read(
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
            case "succeeded":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<MessageBatchSucceededResult>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new MessageBatchSucceededResultVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "errored":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<MessageBatchErroredResult>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new MessageBatchErroredResultVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "canceled":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<MessageBatchCanceledResult>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new MessageBatchCanceledResultVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "expired":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<MessageBatchExpiredResult>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new MessageBatchExpiredResultVariant(deserialized);
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
        MessageBatchResult value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            MessageBatchSucceededResultVariant(var messageBatchSucceededResult) =>
                messageBatchSucceededResult,
            MessageBatchErroredResultVariant(var messageBatchErroredResult) =>
                messageBatchErroredResult,
            MessageBatchCanceledResultVariant(var messageBatchCanceledResult) =>
                messageBatchCanceledResult,
            MessageBatchExpiredResultVariant(var messageBatchExpiredResult) =>
                messageBatchExpiredResult,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
