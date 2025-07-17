using Files = Anthropic.Models.Beta.Files;
using Json = System.Text.Json;
using Tasks = System.Threading.Tasks;

namespace Anthropic.Service.Beta.Files;

public interface IFileService
{
    /// <summary>
    /// List Files
    /// </summary>
    Tasks::Task<Files::FileListPageResponse> List(Files::FileListParams @params);

    /// <summary>
    /// Delete File
    /// </summary>
    Tasks::Task<Files::DeletedFile> Delete(Files::FileDeleteParams @params);

    /// <summary>
    /// Download File
    /// </summary>
    Tasks::Task<Json::JsonElement> Download(Files::FileDownloadParams @params);

    /// <summary>
    /// Get File Metadata
    /// </summary>
    Tasks::Task<Files::FileMetadata> RetrieveMetadata(Files::FileRetrieveMetadataParams @params);

    /// <summary>
    /// Upload File
    /// </summary>
    Tasks::Task<Files::FileMetadata> Upload(Files::FileUploadParams @params);
}
