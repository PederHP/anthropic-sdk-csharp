using System;
using System.Net.Http;
using Anthropic.Services.Beta;
using Anthropic.Services.Completions;
using Anthropic.Services.Messages;
using Anthropic.Services.Models;

namespace Anthropic;

public interface IAnthropicClient
{
    HttpClient HttpClient { get; init; }

    Uri BaseUrl { get; init; }

    string? APIKey { get; init; }

    string? AuthToken { get; init; }

    ICompletionService Completions { get; }

    IMessageService Messages { get; }

    IModelService Models { get; }

    IBetaService Beta { get; }
}
