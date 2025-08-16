namespace Anthropic.Models.Beta.Messages.BetaContentBlockVariants;

public sealed record class BetaTextBlockVariant(BetaTextBlock Value)
    : BetaContentBlock,
        IVariant<BetaTextBlockVariant, BetaTextBlock>
{
    public static BetaTextBlockVariant From(BetaTextBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaThinkingBlockVariant(BetaThinkingBlock Value)
    : BetaContentBlock,
        IVariant<BetaThinkingBlockVariant, BetaThinkingBlock>
{
    public static BetaThinkingBlockVariant From(BetaThinkingBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaRedactedThinkingBlockVariant(BetaRedactedThinkingBlock Value)
    : BetaContentBlock,
        IVariant<BetaRedactedThinkingBlockVariant, BetaRedactedThinkingBlock>
{
    public static BetaRedactedThinkingBlockVariant From(BetaRedactedThinkingBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaToolUseBlockVariant(BetaToolUseBlock Value)
    : BetaContentBlock,
        IVariant<BetaToolUseBlockVariant, BetaToolUseBlock>
{
    public static BetaToolUseBlockVariant From(BetaToolUseBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaServerToolUseBlockVariant(BetaServerToolUseBlock Value)
    : BetaContentBlock,
        IVariant<BetaServerToolUseBlockVariant, BetaServerToolUseBlock>
{
    public static BetaServerToolUseBlockVariant From(BetaServerToolUseBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaWebSearchToolResultBlockVariant(BetaWebSearchToolResultBlock Value)
    : BetaContentBlock,
        IVariant<BetaWebSearchToolResultBlockVariant, BetaWebSearchToolResultBlock>
{
    public static BetaWebSearchToolResultBlockVariant From(BetaWebSearchToolResultBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaCodeExecutionToolResultBlockVariant(
    BetaCodeExecutionToolResultBlock Value
)
    : BetaContentBlock,
        IVariant<BetaCodeExecutionToolResultBlockVariant, BetaCodeExecutionToolResultBlock>
{
    public static BetaCodeExecutionToolResultBlockVariant From(
        BetaCodeExecutionToolResultBlock value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaMCPToolUseBlockVariant(BetaMCPToolUseBlock Value)
    : BetaContentBlock,
        IVariant<BetaMCPToolUseBlockVariant, BetaMCPToolUseBlock>
{
    public static BetaMCPToolUseBlockVariant From(BetaMCPToolUseBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class BetaMCPToolResultBlockVariant(BetaMCPToolResultBlock Value)
    : BetaContentBlock,
        IVariant<BetaMCPToolResultBlockVariant, BetaMCPToolResultBlock>
{
    public static BetaMCPToolResultBlockVariant From(BetaMCPToolResultBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

/// <summary>
/// Response model for a file uploaded to the container.
/// </summary>
public sealed record class BetaContainerUploadBlockVariant(BetaContainerUploadBlock Value)
    : BetaContentBlock,
        IVariant<BetaContainerUploadBlockVariant, BetaContainerUploadBlock>
{
    public static BetaContainerUploadBlockVariant From(BetaContainerUploadBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
