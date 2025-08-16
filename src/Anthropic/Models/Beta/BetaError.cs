using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic.Models.Beta.BetaErrorVariants;

namespace Anthropic.Models.Beta;

[JsonConverter(typeof(BetaErrorConverter))]
public abstract record class BetaError
{
    internal BetaError() { }

    public static implicit operator BetaError(BetaInvalidRequestError value) =>
        new BetaInvalidRequestErrorVariant(value);

    public static implicit operator BetaError(BetaAuthenticationError value) =>
        new BetaAuthenticationErrorVariant(value);

    public static implicit operator BetaError(BetaBillingError value) =>
        new BetaBillingErrorVariant(value);

    public static implicit operator BetaError(BetaPermissionError value) =>
        new BetaPermissionErrorVariant(value);

    public static implicit operator BetaError(BetaNotFoundError value) =>
        new BetaNotFoundErrorVariant(value);

    public static implicit operator BetaError(BetaRateLimitError value) =>
        new BetaRateLimitErrorVariant(value);

    public static implicit operator BetaError(BetaGatewayTimeoutError value) =>
        new BetaGatewayTimeoutErrorVariant(value);

    public static implicit operator BetaError(BetaAPIError value) => new BetaAPIErrorVariant(value);

    public static implicit operator BetaError(BetaOverloadedError value) =>
        new BetaOverloadedErrorVariant(value);

    public abstract void Validate();
}

sealed class BetaErrorConverter : JsonConverter<BetaError>
{
    public override BetaError? Read(
        ref Utf8JsonReader reader,
        Type _typeToConvert,
        JsonSerializerOptions options
    )
    {
        var json = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        string? type;
        try
        {
            type = json.GetProperty("type").GetString();
        }
        catch
        {
            type = null;
        }

        switch (type)
        {
            case "invalid_request_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaInvalidRequestError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaInvalidRequestErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "authentication_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaAuthenticationError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaAuthenticationErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "billing_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaBillingError>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaBillingErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "permission_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaPermissionError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaPermissionErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "not_found_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaNotFoundError>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaNotFoundErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "rate_limit_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaRateLimitError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaRateLimitErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "timeout_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaGatewayTimeoutError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaGatewayTimeoutErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "api_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaAPIError>(json, options);
                    if (deserialized != null)
                    {
                        return new BetaAPIErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            case "overloaded_error":
            {
                List<JsonException> exceptions = [];

                try
                {
                    var deserialized = JsonSerializer.Deserialize<BetaOverloadedError>(
                        json,
                        options
                    );
                    if (deserialized != null)
                    {
                        return new BetaOverloadedErrorVariant(deserialized);
                    }
                }
                catch (JsonException e)
                {
                    exceptions.Add(e);
                }

                throw new AggregateException(exceptions);
            }
            default:
            {
                throw new Exception();
            }
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        BetaError value,
        JsonSerializerOptions options
    )
    {
        object variant = value switch
        {
            BetaInvalidRequestErrorVariant(var betaInvalidRequestError) => betaInvalidRequestError,
            BetaAuthenticationErrorVariant(var betaAuthenticationError) => betaAuthenticationError,
            BetaBillingErrorVariant(var betaBillingError) => betaBillingError,
            BetaPermissionErrorVariant(var betaPermissionError) => betaPermissionError,
            BetaNotFoundErrorVariant(var betaNotFoundError) => betaNotFoundError,
            BetaRateLimitErrorVariant(var betaRateLimitError) => betaRateLimitError,
            BetaGatewayTimeoutErrorVariant(var betaGatewayTimeoutError) => betaGatewayTimeoutError,
            BetaAPIErrorVariant(var betaAPIError) => betaAPIError,
            BetaOverloadedErrorVariant(var betaOverloadedError) => betaOverloadedError,
            _ => throw new ArgumentOutOfRangeException(nameof(value)),
        };
        JsonSerializer.Serialize(writer, variant, options);
    }
}
