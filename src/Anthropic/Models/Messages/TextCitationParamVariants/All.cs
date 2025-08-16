namespace Anthropic.Models.Messages.TextCitationParamVariants;

public sealed record class CitationCharLocationParamVariant(CitationCharLocationParam Value)
    : TextCitationParam,
        IVariant<CitationCharLocationParamVariant, CitationCharLocationParam>
{
    public static CitationCharLocationParamVariant From(CitationCharLocationParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class CitationPageLocationParamVariant(CitationPageLocationParam Value)
    : TextCitationParam,
        IVariant<CitationPageLocationParamVariant, CitationPageLocationParam>
{
    public static CitationPageLocationParamVariant From(CitationPageLocationParam value)
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class CitationContentBlockLocationParamVariant(
    CitationContentBlockLocationParam Value
)
    : TextCitationParam,
        IVariant<CitationContentBlockLocationParamVariant, CitationContentBlockLocationParam>
{
    public static CitationContentBlockLocationParamVariant From(
        CitationContentBlockLocationParam value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class CitationWebSearchResultLocationParamVariant(
    CitationWebSearchResultLocationParam Value
)
    : TextCitationParam,
        IVariant<CitationWebSearchResultLocationParamVariant, CitationWebSearchResultLocationParam>
{
    public static CitationWebSearchResultLocationParamVariant From(
        CitationWebSearchResultLocationParam value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}

public sealed record class CitationSearchResultLocationParamVariant(
    CitationSearchResultLocationParam Value
)
    : TextCitationParam,
        IVariant<CitationSearchResultLocationParamVariant, CitationSearchResultLocationParam>
{
    public static CitationSearchResultLocationParamVariant From(
        CitationSearchResultLocationParam value
    )
    {
        return new(value);
    }

    public override void Validate()
    {
        this.Value.Validate();
    }
}
