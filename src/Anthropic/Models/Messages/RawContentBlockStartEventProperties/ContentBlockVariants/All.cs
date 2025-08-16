namespace Anthropic.Models.Messages.RawContentBlockStartEventProperties.ContentBlockVariants;

public sealed record class TextBlockVariant(TextBlock Value)
    : ContentBlockModel,
        IVariant<TextBlockVariant, TextBlock>
{
    public static TextBlockVariant From(TextBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class ThinkingBlockVariant(ThinkingBlock Value)
    : ContentBlockModel,
        IVariant<ThinkingBlockVariant, ThinkingBlock>
{
    public static ThinkingBlockVariant From(ThinkingBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class RedactedThinkingBlockVariant(RedactedThinkingBlock Value)
    : ContentBlockModel,
        IVariant<RedactedThinkingBlockVariant, RedactedThinkingBlock>
{
    public static RedactedThinkingBlockVariant From(RedactedThinkingBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class ToolUseBlockVariant(ToolUseBlock Value)
    : ContentBlockModel,
        IVariant<ToolUseBlockVariant, ToolUseBlock>
{
    public static ToolUseBlockVariant From(ToolUseBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class ServerToolUseBlockVariant(ServerToolUseBlock Value)
    : ContentBlockModel,
        IVariant<ServerToolUseBlockVariant, ServerToolUseBlock>
{
    public static ServerToolUseBlockVariant From(ServerToolUseBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class WebSearchToolResultBlockVariant(WebSearchToolResultBlock Value)
    : ContentBlockModel,
        IVariant<WebSearchToolResultBlockVariant, WebSearchToolResultBlock>
{
    public static WebSearchToolResultBlockVariant From(WebSearchToolResultBlock value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
