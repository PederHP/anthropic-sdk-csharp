using System.Collections.Generic;

namespace Anthropic.Models.Beta.Messages.BetaWebSearchToolResultBlockParamContentVariants;

public sealed record class ResultBlock(List<BetaWebSearchResultBlockParam> Value)
    : BetaWebSearchToolResultBlockParamContent,
        IVariant<ResultBlock, List<BetaWebSearchResultBlockParam>>
{
    public static ResultBlock From(List<BetaWebSearchResultBlockParam> value)
    {
        return new(value);
    }

    public override void Validate() { }
}

public sealed record class BetaWebSearchToolRequestErrorVariant(BetaWebSearchToolRequestError Value)
    : BetaWebSearchToolResultBlockParamContent,
        IVariant<BetaWebSearchToolRequestErrorVariant, BetaWebSearchToolRequestError>
{
    public static BetaWebSearchToolRequestErrorVariant From(BetaWebSearchToolRequestError value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
