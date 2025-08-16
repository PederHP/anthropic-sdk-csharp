namespace Anthropic.Models.Beta.Messages.BetaTextCitationVariants;

public sealed record class BetaCitationCharLocationVariant(BetaCitationCharLocation Value)
    : BetaTextCitation,
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
    : BetaTextCitation,
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
)
    : BetaTextCitation,
        IVariant<BetaCitationContentBlockLocationVariant, BetaCitationContentBlockLocation>
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
    : BetaTextCitation,
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
)
    : BetaTextCitation,
        IVariant<BetaCitationSearchResultLocationVariant, BetaCitationSearchResultLocation>
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
