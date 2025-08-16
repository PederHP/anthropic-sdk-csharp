namespace Anthropic.Models.Beta.Messages.BetaCitationsDeltaProperties.CitationVariants;

public sealed record class BetaCitationCharLocationVariant(BetaCitationCharLocation Value)
    : Citation,
        IVariant<BetaCitationCharLocationVariant, BetaCitationCharLocation>
{
    public static BetaCitationCharLocationVariant From(BetaCitationCharLocation value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCitationPageLocationVariant(BetaCitationPageLocation Value)
    : Citation,
        IVariant<BetaCitationPageLocationVariant, BetaCitationPageLocation>
{
    public static BetaCitationPageLocationVariant From(BetaCitationPageLocation value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCitationContentBlockLocationVariant(
    BetaCitationContentBlockLocation Value
) : Citation, IVariant<BetaCitationContentBlockLocationVariant, BetaCitationContentBlockLocation>
{
    public static BetaCitationContentBlockLocationVariant From(
        BetaCitationContentBlockLocation value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCitationsWebSearchResultLocationVariant(
    BetaCitationsWebSearchResultLocation Value
)
    : Citation,
        IVariant<BetaCitationsWebSearchResultLocationVariant, BetaCitationsWebSearchResultLocation>
{
    public static BetaCitationsWebSearchResultLocationVariant From(
        BetaCitationsWebSearchResultLocation value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCitationSearchResultLocationVariant(
    BetaCitationSearchResultLocation Value
) : Citation, IVariant<BetaCitationSearchResultLocationVariant, BetaCitationSearchResultLocation>
{
    public static BetaCitationSearchResultLocationVariant From(
        BetaCitationSearchResultLocation value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
