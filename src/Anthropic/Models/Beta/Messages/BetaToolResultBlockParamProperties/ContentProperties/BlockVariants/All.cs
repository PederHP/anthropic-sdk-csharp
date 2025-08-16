namespace Anthropic.Models.Beta.Messages.BetaToolResultBlockParamProperties.ContentProperties.BlockVariants;

public sealed record class BetaTextBlockParamVariant(BetaTextBlockParam Value)
    : Block,
        IVariant<BetaTextBlockParamVariant, BetaTextBlockParam>
{
    public static BetaTextBlockParamVariant From(BetaTextBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaImageBlockParamVariant(BetaImageBlockParam Value)
    : Block,
        IVariant<BetaImageBlockParamVariant, BetaImageBlockParam>
{
    public static BetaImageBlockParamVariant From(BetaImageBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaSearchResultBlockParamVariant(BetaSearchResultBlockParam Value)
    : Block,
        IVariant<BetaSearchResultBlockParamVariant, BetaSearchResultBlockParam>
{
    public static BetaSearchResultBlockParamVariant From(BetaSearchResultBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
