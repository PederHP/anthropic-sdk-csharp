using Models = Anthropic.Models.Beta.Models;
using Tasks = System.Threading.Tasks;

namespace Anthropic.Service.Beta.Models;

public interface IModelService
{
    /// <summary>
    /// Get a specific model.
    ///
    /// The Models API response can be used to determine information about a specific
    /// model or resolve a model alias to a model ID.
    /// </summary>
    Tasks::Task<Models::BetaModelInfo> Retrieve(Models::ModelRetrieveParams @params);

    /// <summary>
    /// List available models.
    ///
    /// The Models API response can be used to determine which models are available
    /// for use in the API. More recently released models are listed first.
    /// </summary>
    Tasks::Task<Models::ModelListPageResponse> List(Models::ModelListParams @params);
}
