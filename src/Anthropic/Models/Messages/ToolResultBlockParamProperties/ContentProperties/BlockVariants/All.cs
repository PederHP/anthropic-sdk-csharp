namespace Anthropic.Models.Messages.ToolResultBlockParamProperties.ContentProperties.BlockVariants;

public sealed record class TextBlockParamVariant(TextBlockParam Value)
    : Block,
        IVariant<TextBlockParamVariant, TextBlockParam>
{
    public static TextBlockParamVariant From(TextBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class ImageBlockParamVariant(ImageBlockParam Value)
    : Block,
        IVariant<ImageBlockParamVariant, ImageBlockParam>
{
    public static ImageBlockParamVariant From(ImageBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class SearchResultBlockParamVariant(SearchResultBlockParam Value)
    : Block,
        IVariant<SearchResultBlockParamVariant, SearchResultBlockParam>
{
    public static SearchResultBlockParamVariant From(SearchResultBlockParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
