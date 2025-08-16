using System.Collections.Generic;

namespace Anthropic.Models.Beta.Messages.BetaContentBlockSourceProperties.ContentVariants;

public sealed record class String(string Value) : Content, IVariant<String, string>
{
    public static String From(string value)
    {
        return new(value);
    }

    public override void Validate() { }
}

public sealed record class BetaContentBlockSourceContentVariant(
    List<BetaContentBlockSourceContent> Value
) : Content, IVariant<BetaContentBlockSourceContentVariant, List<BetaContentBlockSourceContent>>
{
    public static BetaContentBlockSourceContentVariant From(
        List<BetaContentBlockSourceContent> value
    )
    {
        return new(value);
    }

    public override void Validate() { }
}
