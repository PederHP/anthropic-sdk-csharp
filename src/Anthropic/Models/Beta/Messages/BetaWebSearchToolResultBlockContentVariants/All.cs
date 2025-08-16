using System.Collections.Generic;

namespace Anthropic.Models.Beta.Messages.BetaWebSearchToolResultBlockContentVariants;

public sealed record class BetaWebSearchToolResultErrorVariant(BetaWebSearchToolResultError Value)
    : BetaWebSearchToolResultBlockContent,
        IVariant<BetaWebSearchToolResultErrorVariant, BetaWebSearchToolResultError>
{
    public static BetaWebSearchToolResultErrorVariant From(BetaWebSearchToolResultError value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaWebSearchResultBlocks(List<BetaWebSearchResultBlock> Value)
    : BetaWebSearchToolResultBlockContent,
        IVariant<BetaWebSearchResultBlocks, List<BetaWebSearchResultBlock>>
{
    public static BetaWebSearchResultBlocks From(List<BetaWebSearchResultBlock> value)
    {
        return new(value);
    }

    public override void Validate() { }
}
