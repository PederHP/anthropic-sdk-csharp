using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic = Anthropic;

namespace Anthropic.Models.Messages;

[JsonConverter(typeof(Anthropic::ModelConverter<CacheCreation>))]
public sealed record class CacheCreation : Anthropic::ModelBase, Anthropic::IFromRaw<CacheCreation>
{
    /// <summary>
    /// The number of input tokens used to create the 1 hour cache entry.
    /// </summary>
    public required long Ephemeral1hInputTokens
    {
        get
        {
            if (!this.Properties.TryGetValue("ephemeral_1h_input_tokens", out JsonElement element))
                throw new global::System.ArgumentOutOfRangeException(
                    "ephemeral_1h_input_tokens",
                    "Missing required argument"
                );

            return JsonSerializer.Deserialize<long>(
                element,
                Anthropic::ModelBase.SerializerOptions
            );
        }
        set
        {
            this.Properties["ephemeral_1h_input_tokens"] = JsonSerializer.SerializeToElement(value);
        }
    }

    /// <summary>
    /// The number of input tokens used to create the 5 minute cache entry.
    /// </summary>
    public required long Ephemeral5mInputTokens
    {
        get
        {
            if (!this.Properties.TryGetValue("ephemeral_5m_input_tokens", out JsonElement element))
                throw new global::System.ArgumentOutOfRangeException(
                    "ephemeral_5m_input_tokens",
                    "Missing required argument"
                );

            return JsonSerializer.Deserialize<long>(
                element,
                Anthropic::ModelBase.SerializerOptions
            );
        }
        set
        {
            this.Properties["ephemeral_5m_input_tokens"] = JsonSerializer.SerializeToElement(value);
        }
    }

    public override void Validate()
    {
        _ = this.Ephemeral1hInputTokens;
        _ = this.Ephemeral5mInputTokens;
    }

    public CacheCreation() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    CacheCreation(Dictionary<string, JsonElement> properties)
    {
        Properties = properties;
    }
#pragma warning restore CS8618

    public static CacheCreation FromRawUnchecked(Dictionary<string, JsonElement> properties)
    {
        return new(properties);
    }
}
