using Anthropic = Anthropic;
using CodeAnalysis = System.Diagnostics.CodeAnalysis;
using Generic = System.Collections.Generic;
using Json = System.Text.Json;
using Serialization = System.Text.Json.Serialization;
using System = System;

namespace Anthropic.Models.Beta.Files;

[Serialization::JsonConverter(typeof(Anthropic::ModelConverter<FileListPageResponse>))]
public sealed record class FileListPageResponse
    : Anthropic::ModelBase,
        Anthropic::IFromRaw<FileListPageResponse>
{
    /// <summary>
    /// List of file metadata objects.
    /// </summary>
    public required Generic::List<FileMetadata> Data
    {
        get
        {
            if (!this.Properties.TryGetValue("data", out Json::JsonElement element))
                throw new System::ArgumentOutOfRangeException("data", "Missing required argument");

            return Json::JsonSerializer.Deserialize<Generic::List<FileMetadata>>(element)
                ?? throw new System::ArgumentNullException("data");
        }
        set { this.Properties["data"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// ID of the first file in this page of results.
    /// </summary>
    public string? FirstID
    {
        get
        {
            if (!this.Properties.TryGetValue("first_id", out Json::JsonElement element))
                return null;

            return Json::JsonSerializer.Deserialize<string?>(element);
        }
        set { this.Properties["first_id"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// Whether there are more results available.
    /// </summary>
    public bool? HasMore
    {
        get
        {
            if (!this.Properties.TryGetValue("has_more", out Json::JsonElement element))
                return null;

            return Json::JsonSerializer.Deserialize<bool?>(element);
        }
        set { this.Properties["has_more"] = Json::JsonSerializer.SerializeToElement(value); }
    }

    /// <summary>
    /// ID of the last file in this page of results.
    /// </summary>
    public string? LastID
    {
        get
        {
            if (!this.Properties.TryGetValue("last_id", out Json::JsonElement element))
                return null;

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

    public FileListPageResponse() { }

#pragma warning disable CS8618
    [CodeAnalysis::SetsRequiredMembers]
    FileListPageResponse(Generic::Dictionary<string, Json::JsonElement> properties)
    {
        Properties = properties;
    }
#pragma warning restore CS8618

    public static FileListPageResponse FromRawUnchecked(
        Generic::Dictionary<string, Json::JsonElement> properties
    )
    {
        return new(properties);
    }
}
