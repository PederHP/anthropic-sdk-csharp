namespace Anthropic.Models.Beta.Messages.Batches.BetaMessageBatchResultVariants;

public sealed record class BetaMessageBatchSucceededResultVariant(
    BetaMessageBatchSucceededResult Value
)
    : BetaMessageBatchResult,
        IVariant<BetaMessageBatchSucceededResultVariant, BetaMessageBatchSucceededResult>
{
    public static BetaMessageBatchSucceededResultVariant From(BetaMessageBatchSucceededResult value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaMessageBatchErroredResultVariant(BetaMessageBatchErroredResult Value)
    : BetaMessageBatchResult,
        IVariant<BetaMessageBatchErroredResultVariant, BetaMessageBatchErroredResult>
{
    public static BetaMessageBatchErroredResultVariant From(BetaMessageBatchErroredResult value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaMessageBatchCanceledResultVariant(
    BetaMessageBatchCanceledResult Value
)
    : BetaMessageBatchResult,
        IVariant<BetaMessageBatchCanceledResultVariant, BetaMessageBatchCanceledResult>
{
    public static BetaMessageBatchCanceledResultVariant From(BetaMessageBatchCanceledResult value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaMessageBatchExpiredResultVariant(BetaMessageBatchExpiredResult Value)
    : BetaMessageBatchResult,
        IVariant<BetaMessageBatchExpiredResultVariant, BetaMessageBatchExpiredResult>
{
    public static BetaMessageBatchExpiredResultVariant From(BetaMessageBatchExpiredResult value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
