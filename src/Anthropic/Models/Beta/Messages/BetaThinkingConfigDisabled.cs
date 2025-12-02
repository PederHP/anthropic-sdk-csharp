using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(
    typeof(ModelConverter<BetaThinkingConfigDisabled, BetaThinkingConfigDisabledFromRaw>)
)]
public sealed record class BetaThinkingConfigDisabled : ModelBase
{
    public JsonElement Type
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "type"); }
        init { ModelBase.Set(this._rawData, "type", value); }
    }

    public override void Validate()
    {
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"disabled\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
    }

    public BetaThinkingConfigDisabled()
    {
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"disabled\"");
    }

    public BetaThinkingConfigDisabled(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Type = JsonSerializer.Deserialize<JsonElement>("\"disabled\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaThinkingConfigDisabled(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaThinkingConfigDisabled FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaThinkingConfigDisabledFromRaw : IFromRaw<BetaThinkingConfigDisabled>
{
    public BetaThinkingConfigDisabled FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaThinkingConfigDisabled.FromRawUnchecked(rawData);
}
