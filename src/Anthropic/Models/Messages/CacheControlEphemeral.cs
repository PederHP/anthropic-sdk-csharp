using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Messages.CacheControlEphemeralProperties;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(ModelConverter<CacheControlEphemeral>))]
public sealed record class CacheControlEphemeral : ModelBase, IFromRaw<CacheControlEphemeral>
{
    public JsonElement Type
    {
        get
        {
            if (!this.Properties.TryGetValue("type", out JsonElement element))
                throw new ArgumentOutOfRangeException("type", "Missing required argument");

            return JsonSerializer.Deserialize<JsonElement>(element, ModelBase.SerializerOptions);
        }
        set { this.Properties["type"] = JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// The time-to-live for the cache control breakpoint.
    ///
    /// This may be one the following values: - `5m`: 5 minutes - `1h`: 1 hour
    ///
    /// Defaults to `5m`.
    /// </summary>
    public TTL? TTL
    {
        get
        {
            if (!this.Properties.TryGetValue("ttl", out JsonElement element))
                return null;

            return JsonSerializer.Deserialize<TTL?>(element, ModelBase.SerializerOptions);
        }
        set { this.Properties["ttl"] = JsonSerializer.SerializeToElement(value); }
    }

    public override void Validate()
    {
        this.TTL?.Validate();
    }

    public CacheControlEphemeral()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"ephemeral\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    CacheControlEphemeral(Dictionary<string, JsonElement> properties)
    {
        Properties = properties;
    }
#pragma warning restore CS8618

    public static CacheControlEphemeral FromRawUnchecked(Dictionary<string, JsonElement> properties)
    {
        return new(properties);
    }
}
