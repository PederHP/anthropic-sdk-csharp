using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.DocumentBlockParamProperties.SourceVariants;

namespace Anthropic.Models.Messages.DocumentBlockParamProperties;

[JsonConverter(typeof(SourceConverter))]
public abstract record class Source
{
    internal Source() { }

    public static implicit operator Source(Base64PDFSource value) =>
        new Base64PDFSourceVariant(value);

    public static implicit operator Source(PlainTextSource value) =>
        new PlainTextSourceVariant(value);

    public static implicit operator Source(ContentBlockSource value) =>
        new ContentBlockSourceVariant(value);

    public static implicit operator Source(URLPDFSource value) => new URLPDFSourceVariant(value);

    public abstract void Validate();
}

sealed class SourceConverter : JsonConverter<Source>
{
    public override Source? Read(
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
            case "base64":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<Base64PDFSource>(json, options);
                    if (deserialized != null)
                    {
                        return new Base64PDFSourceVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "text":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<PlainTextSource>(json, options);
                    if (deserialized != null)
                    {
                        return new PlainTextSourceVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "content":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<ContentBlockSource>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new ContentBlockSourceVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "url":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<URLPDFSource>(json, options);
                    if (deserialized != null)
                    {
                        return new URLPDFSourceVariant(deserialized);
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

    public override void Write(Utf8JsonWriter writer, Source value, JsonSerializerOptions options)
    {
        object variant = value switch
        {
            Base64PDFSourceVariant(var base64PDFSource) => base64PDFSource,
            PlainTextSourceVariant(var plainTextSource) => plainTextSource,
            ContentBlockSourceVariant(var contentBlockSource) => contentBlockSource,
            URLPDFSourceVariant(var urlpdfSource) => urlpdfSource,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
