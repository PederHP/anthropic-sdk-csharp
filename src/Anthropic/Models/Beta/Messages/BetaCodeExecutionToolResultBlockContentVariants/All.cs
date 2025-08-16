namespace Anthropic.Models.Beta.Messages.BetaCodeExecutionToolResultBlockContentVariants;

public sealed record class BetaCodeExecutionToolResultErrorVariant(
    BetaCodeExecutionToolResultError Value
)
    : BetaCodeExecutionToolResultBlockContent,
        IVariant<BetaCodeExecutionToolResultErrorVariant, BetaCodeExecutionToolResultError>
{
    public static BetaCodeExecutionToolResultErrorVariant From(
        BetaCodeExecutionToolResultError value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCodeExecutionResultBlockVariant(BetaCodeExecutionResultBlock Value)
    : BetaCodeExecutionToolResultBlockContent,
        IVariant<BetaCodeExecutionResultBlockVariant, BetaCodeExecutionResultBlock>
{
    public static BetaCodeExecutionResultBlockVariant From(BetaCodeExecutionResultBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
