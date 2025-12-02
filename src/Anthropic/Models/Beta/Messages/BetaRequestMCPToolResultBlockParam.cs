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

[JsonConverter(
    typeof(ModelConverter<
        BetaRequestMCPToolResultBlockParam,
        BetaRequestMCPToolResultBlockParamFromRaw
    >)
)]
public sealed record class BetaRequestMCPToolResultBlockParam : ModelBase
{
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

    public BetaRequestMCPToolResultBlockParamContent? Content
    {
        get
        {
            return ModelBase.GetNullableClass<BetaRequestMCPToolResultBlockParamContent>(
                this.RawData,
                "content"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "content", value);
        }
    }

    public bool? IsError
    {
        get { return ModelBase.GetNullableStruct<bool>(this.RawData, "is_error"); }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "is_error", value);
        }
    }

    public override void Validate()
    {
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
        this.CacheControl?.Validate();
        this.Content?.Validate();
        _ = this.IsError;
    }

    public BetaRequestMCPToolResultBlockParam()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"mcp_tool_result\"");
    }

    public BetaRequestMCPToolResultBlockParam(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"mcp_tool_result\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaRequestMCPToolResultBlockParam(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaRequestMCPToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public BetaRequestMCPToolResultBlockParam(string toolUseID)
        : this()
    {
        this.ToolUseID = toolUseID;
    }
}

class BetaRequestMCPToolResultBlockParamFromRaw : IFromRaw<BetaRequestMCPToolResultBlockParam>
{
    public BetaRequestMCPToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaRequestMCPToolResultBlockParam.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(BetaRequestMCPToolResultBlockParamContentConverter))]
public record class BetaRequestMCPToolResultBlockParamContent
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public BetaRequestMCPToolResultBlockParamContent(string value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public BetaRequestMCPToolResultBlockParamContent(
        IReadOnlyList<BetaTextBlockParam> value,
        JsonElement? json = null
    )
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._json = json;
    }

    public BetaRequestMCPToolResultBlockParamContent(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickString([NotNullWhen(true)] out string? value)
    {
        value = this.Value as string;
        return value != null;
    }

    public bool TryPickBetaMCPToolResultBlockParam(
        [NotNullWhen(true)] out IReadOnlyList<BetaTextBlockParam>? value
    )
    {
        value = this.Value as IReadOnlyList<BetaTextBlockParam>;
        return value != null;
    }

    public void Switch(
        System::Action<string> @string,
        System::Action<IReadOnlyList<BetaTextBlockParam>> betaMCPToolResultBlockParamContent
    )
    {
        switch (this.Value)
        {
            case string value:
                @string(value);
                break;
            case List<BetaTextBlockParam> value:
                betaMCPToolResultBlockParamContent(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaRequestMCPToolResultBlockParamContent"
                );
        }
    }

    public T Match<T>(
        System::Func<string, T> @string,
        System::Func<IReadOnlyList<BetaTextBlockParam>, T> betaMCPToolResultBlockParamContent
    )
    {
        return this.Value switch
        {
            string value => @string(value),
            IReadOnlyList<BetaTextBlockParam> value => betaMCPToolResultBlockParamContent(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaRequestMCPToolResultBlockParamContent"
            ),
        };
    }

    public static implicit operator BetaRequestMCPToolResultBlockParamContent(string value) =>
        new(value);

    public static implicit operator BetaRequestMCPToolResultBlockParamContent(
        List<BetaTextBlockParam> value
    ) => new((IReadOnlyList<BetaTextBlockParam>)value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaRequestMCPToolResultBlockParamContent"
            );
        }
    }

    public virtual bool Equals(BetaRequestMCPToolResultBlockParamContent? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BetaRequestMCPToolResultBlockParamContentConverter
    : JsonConverter<BetaRequestMCPToolResultBlockParamContent>
{
    public override BetaRequestMCPToolResultBlockParamContent? Read(
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
            var deserialized = JsonSerializer.Deserialize<List<BetaTextBlockParam>>(json, options);
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
        BetaRequestMCPToolResultBlockParamContent value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
