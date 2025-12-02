using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Core;
using Anthropic.Exceptions;
using System = System;

namespace Anthropic.Models.Beta.Messages;

[JsonConverter(typeof(ModelConverter<BetaToolBash20241022, BetaToolBash20241022FromRaw>))]
public sealed record class BetaToolBash20241022 : ModelBase
{
    /// <summary>
    /// Name of the tool.
    ///
    /// <para>This is how the tool will be called by the model and in `tool_use` blocks.</para>
    /// </summary>
    public JsonElement Name
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "name"); }
        init { ModelBase.Set(this._rawData, "name", value); }
    }

    public JsonElement Type
    {
        get { return ModelBase.GetNotNullStruct<JsonElement>(this.RawData, "type"); }
        init { ModelBase.Set(this._rawData, "type", value); }
    }

    public IReadOnlyList<ApiEnum<string, AllowedCaller3>>? AllowedCallers
    {
        get
        {
            return ModelBase.GetNullableClass<List<ApiEnum<string, AllowedCaller3>>>(
                this.RawData,
                "allowed_callers"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "allowed_callers", value);
        }
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

    /// <summary>
    /// If true, tool will not be included in initial system prompt. Only loaded when
    /// returned via tool_reference from tool search.
    /// </summary>
    public bool? DeferLoading
    {
        get { return ModelBase.GetNullableStruct<bool>(this.RawData, "defer_loading"); }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "defer_loading", value);
        }
    }

    public IReadOnlyList<Dictionary<string, JsonElement>>? InputExamples
    {
        get
        {
            return ModelBase.GetNullableClass<List<Dictionary<string, JsonElement>>>(
                this.RawData,
                "input_examples"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "input_examples", value);
        }
    }

    public bool? Strict
    {
        get { return ModelBase.GetNullableStruct<bool>(this.RawData, "strict"); }
        init
        {
            if (value == null)
            {
                return;
            }

            ModelBase.Set(this._rawData, "strict", value);
        }
    }

    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Name, JsonSerializer.Deserialize<JsonElement>("\"bash\"")))
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
        if (
            !JsonElement.DeepEquals(
                this.Type,
                JsonSerializer.Deserialize<JsonElement>("\"bash_20241022\"")
            )
        )
        {
            throw new AnthropicInvalidDataException("Invalid value given for constant");
        }
        foreach (var item in this.AllowedCallers ?? [])
        {
            item.Validate();
        }
        this.CacheControl?.Validate();
        _ = this.DeferLoading;
        _ = this.InputExamples;
        _ = this.Strict;
    }

    public BetaToolBash20241022()
    {
        this.Name = JsonSerializer.Deserialize<JsonElement>("\"bash\"");
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_20241022\"");
    }

    public BetaToolBash20241022(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];

        this.Name = JsonSerializer.Deserialize<JsonElement>("\"bash\"");
        this.Type = JsonSerializer.Deserialize<JsonElement>("\"bash_20241022\"");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    BetaToolBash20241022(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = [.. rawData];
    }
#pragma warning restore CS8618

    public static BetaToolBash20241022 FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class BetaToolBash20241022FromRaw : IFromRaw<BetaToolBash20241022>
{
    public BetaToolBash20241022 FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => BetaToolBash20241022.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(AllowedCaller3Converter))]
public enum AllowedCaller3
{
    Direct,
    CodeExecution20250825,
}

sealed class AllowedCaller3Converter : JsonConverter<AllowedCaller3>
{
    public override AllowedCaller3 Read(
        ref Utf8JsonReader reader,
        System::Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "direct" => AllowedCaller3.Direct,
            "code_execution_20250825" => AllowedCaller3.CodeExecution20250825,
            _ => (AllowedCaller3)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        AllowedCaller3 value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                AllowedCaller3.Direct => "direct",
                AllowedCaller3.CodeExecution20250825 => "code_execution_20250825",
                _ => throw new AnthropicInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
