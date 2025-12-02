using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;
using System = System;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(
    typeof(ModelConverter<
        BetaBashCodeExecutionToolResultBlock,
        BetaBashCodeExecutionToolResultBlockFromRaw
    >)
)]
public sealed record class BetaBashCodeExecutionToolResultBlock : ModelBase
{
    public required Content Content
    {
        get { return ModelBase.GetNotNullClass<Content>(this.RawData, "content"); }
        init { ModelBase.Set(this._rawData, "content", value); }
    }

    public required string ToolUseID
    {
        get { return ModelBase.GetNotNullClass<string>(this.RawData, "tool_use_id"); }
        init { ModelBase.Set(this._rawData, "tool_use_id", value); }
    }

    public JsonElement Type
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "type"); }
        init { ModelBase.Set(this._rawData, "type", value); }
    }

    public override void Validate()
    {
        this.Content.Validate();
        _ = this.ToolUseID;
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"bash_code_execution_tool_result\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
    }

    public BetaBashCodeExecutionToolResultBlock()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_code_execution_tool_result\"");
    }

    public BetaBashCodeExecutionToolResultBlock(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_code_execution_tool_result\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaBashCodeExecutionToolResultBlock(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaBashCodeExecutionToolResultBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaBashCodeExecutionToolResultBlockFromRaw : IFromRaw<BetaBashCodeExecutionToolResultBlock>
{
    public BetaBashCodeExecutionToolResultBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaBashCodeExecutionToolResultBlock.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(ContentConverter))]
public record class Content
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public JsonElement Type
    {
        get
        {
            return Match(
                betaBashCodeExecutionToolResultError: (x) => x.Type,
                betaBashCodeExecutionResultBlock: (x) => x.Type
            );
        }
    }

    public Content(BetaBashCodeExecutionToolResultError value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Content(BetaBashCodeExecutionResultBlock value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Content(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickBetaBashCodeExecutionToolResultError(
        [NotNullWhen(true)] out BetaBashCodeExecutionToolResultError? value
    )
    {
        value = this.Value as BetaBashCodeExecutionToolResultError;
        return value != null;
    }

    public bool TryPickBetaBashCodeExecutionResultBlock(
        [NotNullWhen(true)] out BetaBashCodeExecutionResultBlock? value
    )
    {
        value = this.Value as BetaBashCodeExecutionResultBlock;
        return value != null;
    }

    public void Switch(
        System::Action<BetaBashCodeExecutionToolResultError> betaBashCodeExecutionToolResultError,
        System::Action<BetaBashCodeExecutionResultBlock> betaBashCodeExecutionResultBlock
    )
    {
        switch (this.Value)
        {
            case BetaBashCodeExecutionToolResultError value:
                betaBashCodeExecutionToolResultError(value);
                break;
            case BetaBashCodeExecutionResultBlock value:
                betaBashCodeExecutionResultBlock(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of Content"
                );
        }
    }

    public T Match<T>(
        System::Func<BetaBashCodeExecutionToolResultError, T> betaBashCodeExecutionToolResultError,
        System::Func<BetaBashCodeExecutionResultBlock, T> betaBashCodeExecutionResultBlock
    )
    {
        return this.Value switch
        {
            BetaBashCodeExecutionToolResultError value => betaBashCodeExecutionToolResultError(
                value
            ),
            BetaBashCodeExecutionResultBlock value => betaBashCodeExecutionResultBlock(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of Content"
            ),
        };
    }

    public static implicit operator Content(BetaBashCodeExecutionToolResultError value) =>
        new(value);

    public static implicit operator Content(BetaBashCodeExecutionResultBlock value) => new(value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException("Data did not match any variant of Content");
        }
    }

    public virtual bool Equals(Content? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class ContentConverter : JsonConverter<Content>
{
    public override Content? Read(
        ref Utf8JsonReader reader,
        System::Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var json = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<BetaBashCodeExecutionToolResultError>(
                json,
                options
            );
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, json);
            }
        }
        catch (System::Exception e) when (e is JsonException || e is AnthropicInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<BetaBashCodeExecutionResultBlock>(
                json,
                options
            );
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, json);
            }
        }
        catch (System::Exception e) when (e is JsonException || e is AnthropicInvalidDataException)
        {
            // ignore
        }

        return new(json);
    }

    public override void Write(Utf8JsonWriter writer, Content value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
