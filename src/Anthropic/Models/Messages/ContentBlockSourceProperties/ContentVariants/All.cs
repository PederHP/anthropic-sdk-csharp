using System.Collections.Generic;

namespace Anthropic.Models.Messages.ContentBlockSourceProperties.ContentVariants;

public sealed record class String(string Value) : Content, IVariant<String, string>
{
    public static String From(string value)
    {
        return new(value);
    }

    public override void Validate() { }
}

public sealed record class ContentBlockSourceContentVariant(List<ContentBlockSourceContent> Value)
    : Content,
        IVariant<ContentBlockSourceContentVariant, List<ContentBlockSourceContent>>
{
    public static ContentBlockSourceContentVariant From(List<ContentBlockSourceContent> value)
    {
        return new(value);
    }

    public override void Validate() { }
}
