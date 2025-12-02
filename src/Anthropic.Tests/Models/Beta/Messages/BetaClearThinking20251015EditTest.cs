using System.Text.Json;
using Anthropic.Exceptions;
using Anthropic.Models.Beta.Messages;

namespace Anthropic.Tests.Models.Beta.Messages;

public class BetaClearThinking20251015EditTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new BetaClearThinking20251015Edit
        {
            Type = JsonSerializer.Deserialize<JsonElement>("\"clear_thinking_20251015\""),
            Keep = new BetaThinkingTurns(1),
        };

        JsonElement expectedType = JsonSerializer.Deserialize<JsonElement>(
            "\"clear_thinking_20251015\""
        );
        Keep expectedKeep = new BetaThinkingTurns(1);

        Assert.True(JsonElement.DeepEquals(expectedType, model.Type));
        Assert.Equal(expectedKeep, model.Keep);
    }
}

public class UnionMember2Test : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new UnionMember2();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<UnionMember2>(
            JsonSerializer.Deserialize<JsonElement>("\"all\"")
        );
        constant.Validate();
    }

    [Fact]
    public void InvalidConstantValidationThrows_Works()
    {
        var constant = JsonSerializer.Deserialize<UnionMember2>(
            JsonSerializer.Deserialize<JsonElement>("\"invalid value\"")
        );
        Assert.Throws<AnthropicInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new UnionMember2();
        var json = JsonSerializer.Serialize(constant);
        var deserialized = JsonSerializer.Deserialize<UnionMember2>(json);

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<UnionMember2>(
            JsonSerializer.Deserialize<JsonElement>("\"all\"")
        );
        var json = JsonSerializer.Serialize(constant);
        var deserialized = JsonSerializer.Deserialize<UnionMember2>(json);

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<UnionMember2>(
            JsonSerializer.Deserialize<JsonElement>("\"invalid value\"")
        );
        var json = JsonSerializer.Serialize(constant);
        var deserialized = JsonSerializer.Deserialize<UnionMember2>(json);

        Assert.Equal(constant, deserialized);
    }
}
