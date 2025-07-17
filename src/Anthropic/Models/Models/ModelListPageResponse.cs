using Anthropic = Anthropic;
using CodeAnalysis = System.Diagnostics.CodeAnalysis;
using Generic = System.Collections.Generic;
using Json = System.Text.Json;
using Serialization = System.Text.Json.Serialization;
using System = System;

namespace Anthropic.Models.Models;

[Serialization::JsonConverter(typeof(Anthropic::ModelConverter<ModelListPageResponse>))]
public sealed record class ModelListPageResponse
    : Anthropic::ModelBase,
        Anthropic::IFromRaw<ModelListPageResponse>
{
    public required Generic::List<ModelInfo> Data
    {
        get
        {
            if (!this.Properties.TryGetValue("data", out Json::JsonElement element))
                throw new System::ArgumentOutOfRangeException("data", "Missing required argument");

            return Json::JsonSerializer.Deserialize<Generic::List<ModelInfo>>(element)
                ?? throw new System::ArgumentNullException("data");
        }
        set { this.Properties["data"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// First ID in the `data` list. Can be used as the `before_id` for the previous page.
    /// </summary>
    public required string? FirstID
    {
        get
        {
            if (!this.Properties.TryGetValue("first_id", out Json::JsonElement element))
                throw new System::ArgumentOutOfRangeException(
                    "first_id",
                    "Missing required argument"
                );

            return Json::JsonSerializer.Deserialize<string?>(element);
        }
        set { this.Properties["first_id"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// Indicates if there are more results in the requested page direction.
    /// </summary>
    public required bool HasMore
    {
        get
        {
            if (!this.Properties.TryGetValue("has_more", out Json::JsonElement element))
                throw new System::ArgumentOutOfRangeException(
                    "has_more",
                    "Missing required argument"
                );

            return Json::JsonSerializer.Deserialize<bool>(element);
        }
        set { this.Properties["has_more"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// Last ID in the `data` list. Can be used as the `after_id` for the next page.
    /// </summary>
    public required string? LastID
    {
        get
        {
            if (!this.Properties.TryGetValue("last_id", out Json::JsonElement element))
                throw new System::ArgumentOutOfRangeException(
                    "last_id",
                    "Missing required argument"
                );

            return Json::JsonSerializer.Deserialize<string?>(element);
        }
        set { this.Properties["last_id"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    public override void Validate()
    {
        foreach (var item in this.Data)
        {
            item.Validate();
        }
        _ = this.FirstID;
        _ = this.HasMore;
        _ = this.LastID;
    }

    public ModelListPageResponse() { }

#pragma warning disable CS8618
    [CodeAnalysis::SetsRequiredMembers]
    ModelListPageResponse(Generic::Dictionary<string, Json::JsonElement> properties)
    {
        Properties = properties;
    }
#pragma warning restore CS8618

    public static ModelListPageResponse FromRawUnchecked(
        Generic::Dictionary<string, Json::JsonElement> properties
    )
    {
        return new(properties);
    }
}
