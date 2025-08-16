using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.TextCitationParamVariants;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(TextCitationParamConverter))]
public abstract record class TextCitationParam
{
    internal TextCitationParam() { }

    public static implicit operator TextCitationParam(CitationCharLocationParam value) =>
        new CitationCharLocationParamVariant(value);

    public static implicit operator TextCitationParam(CitationPageLocationParam value) =>
        new CitationPageLocationParamVariant(value);

    public static implicit operator TextCitationParam(CitationContentBlockLocationParam value) =>
        new CitationContentBlockLocationParamVariant(value);

    public static implicit operator TextCitationParam(CitationWebSearchResultLocationParam value) =>
        new CitationWebSearchResultLocationParamVariant(value);

    public static implicit operator TextCitationParam(CitationSearchResultLocationParam value) =>
        new CitationSearchResultLocationParamVariant(value);

    public abstract void Validate();
}

sealed class TextCitationParamConverter : JsonConverter<TextCitationParam>
{
    public override TextCitationParam? Read(
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
                    var deserialized = JsonSerializer.Deserialize<CitationCharLocationParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationCharLocationParamVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<CitationPageLocationParam>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new CitationPageLocationParamVariant(deserialized);
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
                    var deserialized =
                        JsonSerializer.Deserialize<CitationContentBlockLocationParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new CitationContentBlockLocationParamVariant(deserialized);
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
                    var deserialized =
                        JsonSerializer.Deserialize<CitationWebSearchResultLocationParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new CitationWebSearchResultLocationParamVariant(deserialized);
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
                    var deserialized =
                        JsonSerializer.Deserialize<CitationSearchResultLocationParam>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new CitationSearchResultLocationParamVariant(deserialized);
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
        TextCitationParam value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            CitationCharLocationParamVariant(var citationCharLocationParam) =>
                citationCharLocationParam,
            CitationPageLocationParamVariant(var citationPageLocationParam) =>
                citationPageLocationParam,
            CitationContentBlockLocationParamVariant(var citationContentBlockLocationParam) =>
                citationContentBlockLocationParam,
            CitationWebSearchResultLocationParamVariant(var citationWebSearchResultLocationParam) =>
                citationWebSearchResultLocationParam,
            CitationSearchResultLocationParamVariant(var citationSearchResultLocationParam) =>
                citationSearchResultLocationParam,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
