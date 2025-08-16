using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Beta.Messages.BetaCitationsDeltaProperties.CitationVariants;

namespace Anthropic.Models.Beta.Messages.BetaCitationsDeltaProperties;

[JsonConverter(typeof(CitationConverter))]
public abstract record class Citation
{
    internal Citation() { }

    public static implicit operator Citation(BetaCitationCharLocation value) =>
        new BetaCitationCharLocationVariant(value);

    public static implicit operator Citation(BetaCitationPageLocation value) =>
        new BetaCitationPageLocationVariant(value);

    public static implicit operator Citation(BetaCitationContentBlockLocation value) =>
        new BetaCitationContentBlockLocationVariant(value);

    public static implicit operator Citation(BetaCitationsWebSearchResultLocation value) =>
        new BetaCitationsWebSearchResultLocationVariant(value);

    public static implicit operator Citation(BetaCitationSearchResultLocation value) =>
        new BetaCitationSearchResultLocationVariant(value);

    public abstract void Validate();
}

sealed class CitationConverter : JsonConverter<Citation>
{
    public override Citation? Read(
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
                    var deserialized = JsonSerializer.Deserialize<BetaCitationCharLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaCitationCharLocationVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<BetaCitationPageLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaCitationPageLocationVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<BetaCitationContentBlockLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaCitationContentBlockLocationVariant(deserialized);
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
                        JsonSerializer.Deserialize<BetaCitationsWebSearchResultLocation>(
                            json,
                            options
                        );
                    if (deserialized != null)
                    {
                        return new BetaCitationsWebSearchResultLocationVariant(deserialized);
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
                    var deserialized = JsonSerializer.Deserialize<BetaCitationSearchResultLocation>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaCitationSearchResultLocationVariant(deserialized);
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

    public override void Write(Utf8JsonWriter writer, Citation value, JsonSerializerOptions options)
    {
        object variant = value switch
        {
            BetaCitationCharLocationVariant(var betaCitationCharLocation) =>
                betaCitationCharLocation,
            BetaCitationPageLocationVariant(var betaCitationPageLocation) =>
                betaCitationPageLocation,
            BetaCitationContentBlockLocationVariant(var betaCitationContentBlockLocation) =>
                betaCitationContentBlockLocation,
            BetaCitationsWebSearchResultLocationVariant(var betaCitationsWebSearchResultLocation) =>
                betaCitationsWebSearchResultLocation,
            BetaCitationSearchResultLocationVariant(var betaCitationSearchResultLocation) =>
                betaCitationSearchResultLocation,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
