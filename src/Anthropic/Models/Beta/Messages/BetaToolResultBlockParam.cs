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

[JsonConverter(typeof(ModelConverter<BetaToolResultBlockParam, BetaToolResultBlockParamFromRaw>))]
public sealed record class BetaToolResultBlockParam : ModelBase
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

    public BetaToolResultBlockParamContent? Content
    {
        get
        {
            return ModelBase.GetNullableClass<BetaToolResultBlockParamContent>(
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
                JsonSerializer.Deserialize<JsonElement>("\"tool_result\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
        this.CacheControl?.Validate();
        this.Content?.Validate();
        _ = this.IsError;
    }

    public BetaToolResultBlockParam()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"tool_result\"");
    }

    public BetaToolResultBlockParam(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"tool_result\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaToolResultBlockParam(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public BetaToolResultBlockParam(string toolUseID)
        : this()
    {
        this.ToolUseID = toolUseID;
    }
}

class BetaToolResultBlockParamFromRaw : IFromRaw<BetaToolResultBlockParam>
{
    public BetaToolResultBlockParam FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaToolResultBlockParam.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(BetaToolResultBlockParamContentConverter))]
public record class BetaToolResultBlockParamContent
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public BetaToolResultBlockParamContent(string value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public BetaToolResultBlockParamContent(IReadOnlyList<Block> value, JsonElement? json = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._json = json;
    }

    public BetaToolResultBlockParamContent(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickString([NotNullWhen(true)] out string? value)
    {
        value = this.Value as string;
        return value != null;
    }

    public bool TryPickBlocks([NotNullWhen(true)] out IReadOnlyList<Block>? value)
    {
        value = this.Value as IReadOnlyList<Block>;
        return value != null;
    }

    public void Switch(System::Action<string> @string, System::Action<IReadOnlyList<Block>> blocks)
    {
        switch (this.Value)
        {
            case string value:
                @string(value);
                break;
            case List<Block> value:
                blocks(value);
                break;
            default:
                throw new AnthropicInvalidDataException(
                    "Data did not match any variant of BetaToolResultBlockParamContent"
                );
        }
    }

    public T Match<T>(System::Func<string, T> @string, System::Func<IReadOnlyList<Block>, T> blocks)
    {
        return this.Value switch
        {
            string value => @string(value),
            IReadOnlyList<Block> value => blocks(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaToolResultBlockParamContent"
            ),
        };
    }

    public static implicit operator BetaToolResultBlockParamContent(string value) => new(value);

    public static implicit operator BetaToolResultBlockParamContent(List<Block> value) =>
        new((IReadOnlyList<Block>)value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException(
                "Data did not match any variant of BetaToolResultBlockParamContent"
            );
        }
    }

    public virtual bool Equals(BetaToolResultBlockParamContent? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BetaToolResultBlockParamContentConverter
    : JsonConverter<BetaToolResultBlockParamContent>
{
    public override BetaToolResultBlockParamContent? Read(
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
            var deserialized = JsonSerializer.Deserialize<List<Block>>(json, options);
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
        BetaToolResultBlockParamContent value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

/// <summary>
/// Tool reference block that can be included in tool_result content.
/// </summary>
[JsonConverter(typeof(BlockConverter))]
public record class Block
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
                betaTextBlockParam: (x) => x.Type,
                betaImageBlockParam: (x) => x.Type,
                betaSearchResultBlockParam: (x) => x.Type,
                betaRequestDocument: (x) => x.Type,
                betaToolReferenceBlockParam: (x) => x.Type
            );
        }
    }

    public BetaCacheControlEphemeral? CacheControl
    {
        get
        {
            return Match<BetaCacheControlEphemeral?>(
                betaTextBlockParam: (x) => x.CacheControl,
                betaImageBlockParam: (x) => x.CacheControl,
                betaSearchResultBlockParam: (x) => x.CacheControl,
                betaRequestDocument: (x) => x.CacheControl,
                betaToolReferenceBlockParam: (x) => x.CacheControl
            );
        }
    }

    public string? Title
    {
        get
        {
            return Match<string?>(
                betaTextBlockParam: (_) => null,
                betaImageBlockParam: (_) => null,
                betaSearchResultBlockParam: (x) => x.Title,
                betaRequestDocument: (x) => x.Title,
                betaToolReferenceBlockParam: (_) => null
            );
        }
    }

    public Block(BetaTextBlockParam value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Block(BetaImageBlockParam value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Block(BetaSearchResultBlockParam value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Block(BetaRequestDocumentBlock value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Block(BetaToolReferenceBlockParam value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Block(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickBetaTextBlockParam([NotNullWhen(true)] out BetaTextBlockParam? value)
    {
        value = this.Value as BetaTextBlockParam;
        return value != null;
    }

    public bool TryPickBetaImageBlockParam([NotNullWhen(true)] out BetaImageBlockParam? value)
    {
        value = this.Value as BetaImageBlockParam;
        return value != null;
    }

    public bool TryPickBetaSearchResultBlockParam(
        [NotNullWhen(true)] out BetaSearchResultBlockParam? value
    )
    {
        value = this.Value as BetaSearchResultBlockParam;
        return value != null;
    }

    public bool TryPickBetaRequestDocument([NotNullWhen(true)] out BetaRequestDocumentBlock? value)
    {
        value = this.Value as BetaRequestDocumentBlock;
        return value != null;
    }

    public bool TryPickBetaToolReferenceBlockParam(
        [NotNullWhen(true)] out BetaToolReferenceBlockParam? value
    )
    {
        value = this.Value as BetaToolReferenceBlockParam;
        return value != null;
    }

    public void Switch(
        System::Action<BetaTextBlockParam> betaTextBlockParam,
        System::Action<BetaImageBlockParam> betaImageBlockParam,
        System::Action<BetaSearchResultBlockParam> betaSearchResultBlockParam,
        System::Action<BetaRequestDocumentBlock> betaRequestDocument,
        System::Action<BetaToolReferenceBlockParam> betaToolReferenceBlockParam
    )
    {
        switch (this.Value)
        {
            case BetaTextBlockParam value:
                betaTextBlockParam(value);
                break;
            case BetaImageBlockParam value:
                betaImageBlockParam(value);
                break;
            case BetaSearchResultBlockParam value:
                betaSearchResultBlockParam(value);
                break;
            case BetaRequestDocumentBlock value:
                betaRequestDocument(value);
                break;
            case BetaToolReferenceBlockParam value:
                betaToolReferenceBlockParam(value);
                break;
            default:
                throw new AnthropicInvalidDataException("Data did not match any variant of Block");
        }
    }

    public T Match<T>(
        System::Func<BetaTextBlockParam, T> betaTextBlockParam,
        System::Func<BetaImageBlockParam, T> betaImageBlockParam,
        System::Func<BetaSearchResultBlockParam, T> betaSearchResultBlockParam,
        System::Func<BetaRequestDocumentBlock, T> betaRequestDocument,
        System::Func<BetaToolReferenceBlockParam, T> betaToolReferenceBlockParam
    )
    {
        return this.Value switch
        {
            BetaTextBlockParam value => betaTextBlockParam(value),
            BetaImageBlockParam value => betaImageBlockParam(value),
            BetaSearchResultBlockParam value => betaSearchResultBlockParam(value),
            BetaRequestDocumentBlock value => betaRequestDocument(value),
            BetaToolReferenceBlockParam value => betaToolReferenceBlockParam(value),
            _ => throw new AnthropicInvalidDataException("Data did not match any variant of Block"),
        };
    }

    public static implicit operator Block(BetaTextBlockParam value) => new(value);

    public static implicit operator Block(BetaImageBlockParam value) => new(value);

    public static implicit operator Block(BetaSearchResultBlockParam value) => new(value);

    public static implicit operator Block(BetaRequestDocumentBlock value) => new(value);

    public static implicit operator Block(BetaToolReferenceBlockParam value) => new(value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException("Data did not match any variant of Block");
        }
    }

    public virtual bool Equals(Block? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class BlockConverter : JsonConverter<Block>
{
    public override Block? Read(
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
            case "text":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaTextBlockParam>(
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
            case "image":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaImageBlockParam>(
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
            case "search_result":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaSearchResultBlockParam>(
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
            case "document":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaRequestDocumentBlock>(
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
            case "tool_reference":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaToolReferenceBlockParam>(
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
                return new Block(json);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Block value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
