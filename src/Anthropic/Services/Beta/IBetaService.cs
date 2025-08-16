using Anthropic.Services.Beta.Files;
using Anthropic.Services.Beta.Messages;
using Anthropic.Services.Beta.Models;

namespace Anthropic.Services.Beta;

public interface IBetaService
{
    IModelService Models { get; }

    IMessageService Messages { get; }

    IFileService Files { get; }
}
