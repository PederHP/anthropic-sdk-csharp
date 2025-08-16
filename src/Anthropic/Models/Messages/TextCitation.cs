using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.TextCitationVariants;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(TextCitationConverter))]
public abstract record class TextCitation
{
    internal TextCitation() { }

    public static implicit operator TextCitation(CitationCharLocation value) =>
        new CitationCharLocationVariant(value);

    public static implicit operator TextCitation(CitationPageLocation value) =>
        new CitationPageLocationVariant(value);

    public static implicit operator TextCitation(CitationContentBlockLocation value) =>
        new CitationContentBlockLocationVariant(value);

    public static implicit operator TextCitation(CitationsWebSearchResultLocation value) =>
        new CitationsWebSearchResultLocationVariant(value);

    public static implicit operator TextCitation(CitationsSearchResultLocation value) =>
        new CitationsSearchResultLocationVariant(value);

    public abstract void Validate();
}

sealed class TextCitationConverter : JsonConverter<TextCitation>
{
    public override TextCitation? Read(
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
            case "char_location":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<CitationCharLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationCharLocationVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "page_location":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<CitationPageLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationPageLocationVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "content_block_location":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<CitationContentBlockLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationContentBlockLocationVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "web_search_result_location":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<CitationsWebSearchResultLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationsWebSearchResultLocationVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "search_result_location":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<CitationsSearchResultLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationsSearchResultLocationVariant(deserialized);
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
        TextCitation value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            CitationCharLocationVariant(var citationCharLocation) => citationCharLocation,
            CitationPageLocationVariant(var citationPageLocation) => citationPageLocation,
            CitationContentBlockLocationVariant(var citationContentBlockLocation) =>
                citationContentBlockLocation,
            CitationsWebSearchResultLocationVariant(var citationsWebSearchResultLocation) =>
                citationsWebSearchResultLocation,
            CitationsSearchResultLocationVariant(var citationsSearchResultLocation) =>
                citationsSearchResultLocation,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
