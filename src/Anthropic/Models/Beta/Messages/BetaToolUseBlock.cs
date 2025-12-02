using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;
using System = System;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(typeof(ModelConverter<BetaToolUseBlock, BetaToolUseBlockFromRaw>))]
public sealed record class BetaToolUseBlock : ModelBase
{
    public required string ID
    {
        get { return ModelBase.GetNotNullClass<string>(this.RawData, "id"); }
        init { ModelBase.Set(this._rawData, "id", value); }
    }

    public required IReadOnlyDictionary<string, JsonElement> Input
    {
        get
        {
            return ModelBase.GetNotNullClass<Dictionary<string, JsonElement>>(
                this.RawData,
                "input"
            );
        }
        init { ModelBase.Set(this._rawData, "input", value); }
    }

    public required string Name
    {
        get { return ModelBase.GetNotNullClass<string>(this.RawData, "name"); }
        init { ModelBase.Set(this._rawData, "name", value); }
    }

    public JsonElement Type
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "type"); }
        init { ModelBase.Set(this._rawData, "type", value); }
    }

    /// <summary>
    /// Tool invocation directly from the model.
    /// </summary>
    public BetaToolUseBlockCaller? Caller
    {
        get { return ModelBase.GetNullableClass<BetaToolUseBlockCaller>(this.RawData, "caller"); }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "caller", value);
        }
    }

    public override void Validate()
    {
        _ = this.ID;
        _ = this.Input;
        _ = this.Name;
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"tool_use\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
        this.Caller?.Validate();
    }

    public BetaToolUseBlock()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"tool_use\"");
    }

    public BetaToolUseBlock(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"tool_use\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaToolUseBlock(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaToolUseBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaToolUseBlockFromRaw : IFromRaw<BetaToolUseBlock>
{
    public BetaToolUseBlock FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        BetaToolUseBlock.FromRawUnchecked(rawData);
}

/// <summary>
/// Tool invocation directly from the model.
/// </summary>
[JsonConverter(typeof(BetaToolUseBlockCallerConverter))]
public record class BetaToolUseBlockCaller
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public JsonElement Type
    {
        get { return Match(betaDirect: (x) => x.Type, betaServerTool: (x) => x.Type); }
    }

    public BetaToolUseBlockCaller(BetaDirectCaller value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public BetaToolUseBlockCaller(BetaServerToolCaller value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public BetaToolUseBlockCaller(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickBetaDirect([NotNullWhen(true)] out BetaDirectCaller? value)
    {
        value = this.Value as BetaDirectCaller;
        return value != null;
    }

    public bool TryPickBetaServerTool([NotNullWhen(true)] out BetaServerToolCaller? value)
    {
        value = this.Value as BetaServerToolCaller;
        return value != null;
    }

    public void Switch(
        System::Action<BetaDirectCaller> betaDirect,
        System::Action<BetaServerToolCaller> betaServerTool
    )
    {
        switch (this.Value)
        {
            case BetaDirectCaller value:
                betaDirect(value);
                break;
            case BetaServerToolCaller value:
                betaServerTool(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaToolUseBlockCaller"
                );
        }
    }

    public T Match<T>(
        System::Func<BetaDirectCaller, T> betaDirect,
        System::Func<BetaServerToolCaller, T> betaServerTool
    )
    {
        return this.Value switch
        {
            BetaDirectCaller value => betaDirect(value),
            BetaServerToolCaller value => betaServerTool(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaToolUseBlockCaller"
            ),
        };
    }

    public static implicit operator BetaToolUseBlockCaller(BetaDirectCaller value) => new(value);

    public static implicit operator BetaToolUseBlockCaller(BetaServerToolCaller value) =>
        new(value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaToolUseBlockCaller"
            );
        }
    }

    public virtual bool Equals(BetaToolUseBlockCaller? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BetaToolUseBlockCallerConverter : JsonConverter<BetaToolUseBlockCaller>
{
    public override BetaToolUseBlockCaller? Read(
        ref Utf8JsonReader reader,
        System::Type typeToConvert,
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
            case "direct":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaDirectCaller>(json, options);
                    if (deserialized != null)
                    {
                        deserialized.Validate();
                        return new(deserialized, json);
                    }
                }
                catch (System::Exception e)
                    when (e is JsonException || e is AnthropicInvalidDataException)
                {
                    // ignore
                }

                return new(json);
            }
            case "code_execution_20250825":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaServerToolCaller>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        deserialized.Validate();
                        return new(deserialized, json);
                    }
                }
                catch (System::Exception e)
                    when (e is JsonException || e is AnthropicInvalidDataException)
                {
                    // ignore
                }

                return new(json);
            }
            default:
            {
                return new BetaToolUseBlockCaller(json);
            }
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        BetaToolUseBlockCaller value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
