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
        BetaBashCodeExecutionToolResultBlockParam,
        BetaBashCodeExecutionToolResultBlockParamFromRaw
    >)
)]
public sealed record class BetaBashCodeExecutionToolResultBlockParam : ModelBase
{
    public required BetaBashCodeExecutionToolResultBlockParamContent Content
    {
        get
        {
            return ModelBase.GetNotNullClass<BetaBashCodeExecutionToolResultBlockParamContent>(
                this.RawData,
                "content"
            );
        }
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

    /// <summary>
    /// Create a cache control breakpoint at this content block.
    /// </summary>
    public BetaCacheControlEphemeral? CacheControl
    {
        get
        {
            return ModelBase.GetNullableClass<BetaCacheControlEphemeral>(
                this.RawData,
                "cache_control"
            );
        }
        init { ModelBase.Set(this._rawData, "cache_control", value); }
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
        this.CacheControl?.Validate();
    }

    public BetaBashCodeExecutionToolResultBlockParam()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_code_execution_tool_result\"");
    }

    public BetaBashCodeExecutionToolResultBlockParam(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_code_execution_tool_result\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaBashCodeExecutionToolResultBlockParam(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaBashCodeExecutionToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaBashCodeExecutionToolResultBlockParamFromRaw
    : IFromRaw<BetaBashCodeExecutionToolResultBlockParam>
{
    public BetaBashCodeExecutionToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaBashCodeExecutionToolResultBlockParam.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(BetaBashCodeExecutionToolResultBlockParamContentConverter))]
public record class BetaBashCodeExecutionToolResultBlockParamContent
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
                betaBashCodeExecutionToolResultErrorParam: (x) => x.Type,
                betaBashCodeExecutionResultBlockParam: (x) => x.Type
            );
        }
    }

    public BetaBashCodeExecutionToolResultBlockParamContent(
        BetaBashCodeExecutionToolResultErrorParam value,
        JsonElement? json = null
    )
    {
        this.Value = value;
        this._json = json;
    }

    public BetaBashCodeExecutionToolResultBlockParamContent(
        BetaBashCodeExecutionResultBlockParam value,
        JsonElement? json = null
    )
    {
        this.Value = value;
        this._json = json;
    }

    public BetaBashCodeExecutionToolResultBlockParamContent(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickBetaBashCodeExecutionToolResultErrorParam(
        [NotNullWhen(true)] out BetaBashCodeExecutionToolResultErrorParam? value
    )
    {
        value = this.Value as BetaBashCodeExecutionToolResultErrorParam;
        return value != null;
    }

    public bool TryPickBetaBashCodeExecutionResultBlockParam(
        [NotNullWhen(true)] out BetaBashCodeExecutionResultBlockParam? value
    )
    {
        value = this.Value as BetaBashCodeExecutionResultBlockParam;
        return value != null;
    }

    public void Switch(
        System::Action<BetaBashCodeExecutionToolResultErrorParam> betaBashCodeExecutionToolResultErrorParam,
        System::Action<BetaBashCodeExecutionResultBlockParam> betaBashCodeExecutionResultBlockParam
    )
    {
        switch (this.Value)
        {
            case BetaBashCodeExecutionToolResultErrorParam value:
                betaBashCodeExecutionToolResultErrorParam(value);
                break;
            case BetaBashCodeExecutionResultBlockParam value:
                betaBashCodeExecutionResultBlockParam(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaBashCodeExecutionToolResultBlockParamContent"
                );
        }
    }

    public T Match<T>(
        System::Func<
            BetaBashCodeExecutionToolResultErrorParam,
            T
        > betaBashCodeExecutionToolResultErrorParam,
        System::Func<BetaBashCodeExecutionResultBlockParam, T> betaBashCodeExecutionResultBlockParam
    )
    {
        return this.Value switch
        {
            BetaBashCodeExecutionToolResultErrorParam value =>
                betaBashCodeExecutionToolResultErrorParam(value),
            BetaBashCodeExecutionResultBlockParam value => betaBashCodeExecutionResultBlockParam(
                value
            ),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaBashCodeExecutionToolResultBlockParamContent"
            ),
        };
    }

    public static implicit operator BetaBashCodeExecutionToolResultBlockParamContent(
        BetaBashCodeExecutionToolResultErrorParam value
    ) => new(value);

    public static implicit operator BetaBashCodeExecutionToolResultBlockParamContent(
        BetaBashCodeExecutionResultBlockParam value
    ) => new(value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaBashCodeExecutionToolResultBlockParamContent"
            );
        }
    }

    public virtual bool Equals(BetaBashCodeExecutionToolResultBlockParamContent? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BetaBashCodeExecutionToolResultBlockParamContentConverter
    : JsonConverter<BetaBashCodeExecutionToolResultBlockParamContent>
{
    public override BetaBashCodeExecutionToolResultBlockParamContent? Read(
        ref Utf8JsonReader reader,
        System::Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var json = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized =
                JsonSerializer.Deserialize<BetaBashCodeExecutionToolResultErrorParam>(
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
            var deserialized = JsonSerializer.Deserialize<BetaBashCodeExecutionResultBlockParam>(
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

    public override void Write(
        Utf8JsonWriter writer,
        BetaBashCodeExecutionToolResultBlockParamContent value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
