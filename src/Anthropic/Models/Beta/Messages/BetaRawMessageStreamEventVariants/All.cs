namespace Anthropic.Models.Beta.Messages.BetaRawMessageStreamEventVariants;

public sealed record class BetaRawMessageStartEventVariant(BetaRawMessageStartEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawMessageStartEventVariant, BetaRawMessageStartEvent>
{
    public static BetaRawMessageStartEventVariant From(BetaRawMessageStartEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRawMessageDeltaEventVariant(BetaRawMessageDeltaEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawMessageDeltaEventVariant, BetaRawMessageDeltaEvent>
{
    public static BetaRawMessageDeltaEventVariant From(BetaRawMessageDeltaEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRawMessageStopEventVariant(BetaRawMessageStopEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawMessageStopEventVariant, BetaRawMessageStopEvent>
{
    public static BetaRawMessageStopEventVariant From(BetaRawMessageStopEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRawContentBlockStartEventVariant(BetaRawContentBlockStartEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawContentBlockStartEventVariant, BetaRawContentBlockStartEvent>
{
    public static BetaRawContentBlockStartEventVariant From(BetaRawContentBlockStartEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRawContentBlockDeltaEventVariant(BetaRawContentBlockDeltaEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawContentBlockDeltaEventVariant, BetaRawContentBlockDeltaEvent>
{
    public static BetaRawContentBlockDeltaEventVariant From(BetaRawContentBlockDeltaEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRawContentBlockStopEventVariant(BetaRawContentBlockStopEvent Value)
    : BetaRawMessageStreamEvent,
        IVariant<BetaRawContentBlockStopEventVariant, BetaRawContentBlockStopEvent>
{
    public static BetaRawContentBlockStopEventVariant From(BetaRawContentBlockStopEvent value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
