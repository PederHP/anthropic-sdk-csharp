using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;
using System = System;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(typeof(ModelConverter<BetaDocumentBlock, BetaDocumentBlockFromRaw>))]
public sealed record class BetaDocumentBlock : ModelBase
{
    /// <summary>
    /// Citation configuration for the document
    /// </summary>
    public required BetaCitationConfig? Citations
    {
        get { return ModelBase.GetNullableClass<BetaCitationConfig>(this.RawData, "citations"); }
        init { ModelBase.Set(this._rawData, "citations", value); }
    }

    public required Source Source
    {
        get { return ModelBase.GetNotNullClass<Source>(this.RawData, "source"); }
        init { ModelBase.Set(this._rawData, "source", value); }
    }

    /// <summary>
    /// The title of the document
    /// </summary>
    public required string? Title
    {
        get { return ModelBase.GetNullableClass<string>(this.RawData, "title"); }
        init { ModelBase.Set(this._rawData, "title", value); }
    }

    public JsonElement Type
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "type"); }
        init { ModelBase.Set(this._rawData, "type", value); }
    }

    public override void Validate()
    {
        this.Citations?.Validate();
        this.Source.Validate();
        _ = this.Title;
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"document\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
    }

    public BetaDocumentBlock()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"document\"");
    }

    public BetaDocumentBlock(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"document\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaDocumentBlock(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaDocumentBlock FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaDocumentBlockFromRaw : IFromRaw<BetaDocumentBlock>
{
    public BetaDocumentBlock FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        BetaDocumentBlock.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(SourceConverter))]
public record class Source
{
    public object? Value { get; } = null;

    JsonElement? _json = null;

    public JsonElement Json
    {
        get { return this._json ??= JsonSerializer.SerializeToElement(this.Value); }
    }

    public string Data
    {
        get { return Match(betaBase64PDF: (x) => x.Data, betaPlainText: (x) => x.Data); }
    }

    public JsonElement MediaType
    {
        get { return Match(betaBase64PDF: (x) => x.MediaType, betaPlainText: (x) => x.MediaType); }
    }

    public JsonElement Type
    {
        get { return Match(betaBase64PDF: (x) => x.Type, betaPlainText: (x) => x.Type); }
    }

    public Source(BetaBase64PDFSource value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Source(BetaPlainTextSource value, JsonElement? json = null)
    {
        this.Value = value;
        this._json = json;
    }

    public Source(JsonElement json)
    {
        this._json = json;
    }

    public bool TryPickBetaBase64PDF([NotNullWhen(true)] out BetaBase64PDFSource? value)
    {
        value = this.Value as BetaBase64PDFSource;
        return value != null;
    }

    public bool TryPickBetaPlainText([NotNullWhen(true)] out BetaPlainTextSource? value)
    {
        value = this.Value as BetaPlainTextSource;
        return value != null;
    }

    public void Switch(
        System::Action<BetaBase64PDFSource> betaBase64PDF,
        System::Action<BetaPlainTextSource> betaPlainText
    )
    {
        switch (this.Value)
        {
            case BetaBase64PDFSource value:
                betaBase64PDF(value);
                break;
            case BetaPlainTextSource value:
                betaPlainText(value);
                break;
            default:
                throw new AnthropicInvalidDataException("Data did not match any variant of Source");
        }
    }

    public T Match<T>(
        System::Func<BetaBase64PDFSource, T> betaBase64PDF,
        System::Func<BetaPlainTextSource, T> betaPlainText
    )
    {
        return this.Value switch
        {
            BetaBase64PDFSource value => betaBase64PDF(value),
            BetaPlainTextSource value => betaPlainText(value),
            _ => throw new AnthropicInvalidDataException(
                "Data did not match any variant of Source"
            ),
        };
    }

    public static implicit operator Source(BetaBase64PDFSource value) => new(value);

    public static implicit operator Source(BetaPlainTextSource value) => new(value);

    public void Validate()
    {
        if (this.Value == null)
        {
            throw new AnthropicInvalidDataException("Data did not match any variant of Source");
        }
    }

    public virtual bool Equals(Source? other)
    {
        return other != null && JsonElement.DeepEquals(this.Json, other.Json);
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

sealed class SourceConverter : JsonConverter<Source>
{
    public override Source? Read(
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
            case "base64":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaBase64PDFSource>(
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
            case "text":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaPlainTextSource>(
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
                return new Source(json);
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, Source value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
