using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;
using System = System;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(typeof(ModelConverter<BetaMCPToolResultBlock, BetaMCPToolResultBlockFromRaw>))]
public sealed record class BetaMCPToolResultBlock : ModelBase
{
    public required BetaMCPToolResultBlockContent Content
    {
        get
        {
            return ModelBase.GetNotNullClass<BetaMCPToolResultBlockContent>(
                this.RawData,
                "content"
            );
        }
        init { ModelBase.Set(this._rawData, "content", value); }
    }

    public required bool IsError
    {
        get { return ModelBase.GetNotNullStruct<bool>(this.RawData, "is_error"); }
        init { ModelBase.Set(this._rawData, "is_error", value); }
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
        _ = this.IsError;
        _ = this.ToolUseID;
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"mcp_tool_result\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
    }

    public BetaMCPToolResultBlock()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"mcp_tool_result\"");
    }

    public BetaMCPToolResultBlock(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"mcp_tool_result\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaMCPToolResultBlock(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaMCPToolResultBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaMCPToolResultBlockFromRaw : IFromRaw<BetaMCPToolResultBlock>
{
    public BetaMCPToolResultBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaMCPToolResultBlock.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(BetaMCPToolResultBlockContentConverter))]
public record class BetaMCPToolResultBlockContent
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public BetaMCPToolResultBlockContent(string value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public BetaMCPToolResultBlockContent(
        IReadOnlyList<BetaTextBlock> value,
        JsonElement? json = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._json = json;
    }

    public BetaMCPToolResultBlockContent(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickString([NotNullWhen(true)] out string? value)
    {
        value = this.Value as string;
        return value != null;
    }

    public bool TryPickBetaMCPToolResultBlock(
        [NotNullWhen(true)] out IReadOnlyList<BetaTextBlock>? value
    )
    {
        value = this.Value as IReadOnlyList<BetaTextBlock>;
        return value != null;
    }

    public void Switch(
        System::Action<string> @string,
        System::Action<IReadOnlyList<BetaTextBlock>> betaMCPToolResultBlockContent
    )
    {
        switch (this.Value)
        {
            case string value:
                @string(value);
                break;
            case List<BetaTextBlock> value:
                betaMCPToolResultBlockContent(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaMCPToolResultBlockContent"
                );
        }
    }

    public T Match<T>(
        System::Func<string, T> @string,
        System::Func<IReadOnlyList<BetaTextBlock>, T> betaMCPToolResultBlockContent
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            IReadOnlyList<BetaTextBlock> value => betaMCPToolResultBlockContent(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaMCPToolResultBlockContent"
            ),
        };
    }

    public static implicit operator BetaMCPToolResultBlockContent(string value) => new(value);

    public static implicit operator BetaMCPToolResultBlockContent(List<BetaTextBlock> value) =>
        new((IReadOnlyList<BetaTextBlock>)value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaMCPToolResultBlockContent"
            );
        }
    }

    public virtual bool Equals(BetaMCPToolResultBlockContent? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BetaMCPToolResultBlockContentConverter : JsonConverter<BetaMCPToolResultBlockContent>
{
    public override BetaMCPToolResultBlockContent? Read(
        ref Utf8JsonReader reader,
        System::Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var json = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<string>(json, options);
            if (deserialized != null)
            {
                return new(deserialized, json);
            }
        }
        catch (System::Exception e) when (e is JsonException || e is AnthropicInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<BetaTextBlock>>(json, options);
            if (deserialized != null)
            {
                return new(deserialized, json);
            }
        }
        catch (System::Exception e) when (e is JsonException || e is AnthropicInvalidDataException)
        {
            // ignore
        }

        return new(json);
    }

    public override void Write(
        Utf8JsonWriter writer,
        BetaMCPToolResultBlockContent value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
