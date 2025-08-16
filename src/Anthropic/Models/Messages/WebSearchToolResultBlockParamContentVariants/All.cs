using System.Collections.Generic;

namespace Anthropic.Models.Messages.WebSearchToolResultBlockParamContentVariants;

public sealed record class WebSearchToolResultBlockItem(List<WebSearchResultBlockParam> Value)
    : WebSearchToolResultBlockParamContent,
        IVariant<WebSearchToolResultBlockItem, List<WebSearchResultBlockParam>>
{
    public static WebSearchToolResultBlockItem From(List<WebSearchResultBlockParam> value)
    {
        return new(value);
    }

    public override void Validate() { }
}

public sealed record class WebSearchToolRequestErrorVariant(WebSearchToolRequestError Value)
    : WebSearchToolResultBlockParamContent,
        IVariant<WebSearchToolRequestErrorVariant, WebSearchToolRequestError>
{
    public static WebSearchToolRequestErrorVariant From(WebSearchToolRequestError value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
