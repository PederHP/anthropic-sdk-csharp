namespace Anthropic.Models.Beta.Messages.BetaRawContentBlockDeltaVariants;

public sealed record class BetaTextDeltaVariant(BetaTextDelta Value)
    : BetaRawContentBlockDelta,
        IVariant<BetaTextDeltaVariant, BetaTextDelta>
{
    public static BetaTextDeltaVariant From(BetaTextDelta value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaInputJSONDeltaVariant(BetaInputJSONDelta Value)
    : BetaRawContentBlockDelta,
        IVariant<BetaInputJSONDeltaVariant, BetaInputJSONDelta>
{
    public static BetaInputJSONDeltaVariant From(BetaInputJSONDelta value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCitationsDeltaVariant(BetaCitationsDelta Value)
    : BetaRawContentBlockDelta,
        IVariant<BetaCitationsDeltaVariant, BetaCitationsDelta>
{
    public static BetaCitationsDeltaVariant From(BetaCitationsDelta value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaThinkingDeltaVariant(BetaThinkingDelta Value)
    : BetaRawContentBlockDelta,
        IVariant<BetaThinkingDeltaVariant, BetaThinkingDelta>
{
    public static BetaThinkingDeltaVariant From(BetaThinkingDelta value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaSignatureDeltaVariant(BetaSignatureDelta Value)
    : BetaRawContentBlockDelta,
        IVariant<BetaSignatureDeltaVariant, BetaSignatureDelta>
{
    public static BetaSignatureDeltaVariant From(BetaSignatureDelta value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
