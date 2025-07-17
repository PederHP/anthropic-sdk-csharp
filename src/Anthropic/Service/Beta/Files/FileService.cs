using Anthropic = Anthropic;
using Files = Anthropic.Models.Beta.Files;
using Http = System.Net.Http;
using Json = System.Text.Json;
using System = System;
using Tasks = System.Threading.Tasks;

namespace Anthropic.Service.Beta.Files;

public sealed class FileService : IFileService
{
    readonly Anthropic::IAnthropicClient _client;

    public FileService(Anthropic::IAnthropicClient client)
    {
        _client = client;
    }

    public async Tasks::Task<Files::FileListPageResponse> List(Files::FileListParams @params)
    {
        Http::HttpRequestMessage webRequest = new(Http::HttpMethod.Get, @params.Url(this._client));
        @params.AddHeadersToRequest(webRequest, this._client);
        using Http::HttpResponseMessage response = await _client.HttpClient.SendAsync(webRequest);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Http::HttpRequestException e)
        {
            throw new Anthropic::HttpException(
                e.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        return Json::JsonSerializer.Deserialize<Files::FileListPageResponse>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Files::DeletedFile> Delete(Files::FileDeleteParams @params)
    {
        Http::HttpRequestMessage webRequest = new(
            Http::HttpMethod.Delete,
            @params.Url(this._client)
        );
        @params.AddHeadersToRequest(webRequest, this._client);
        using Http::HttpResponseMessage response = await _client.HttpClient.SendAsync(webRequest);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Http::HttpRequestException e)
        {
            throw new Anthropic::HttpException(
                e.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        return Json::JsonSerializer.Deserialize<Files::DeletedFile>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Json::JsonElement> Download(Files::FileDownloadParams @params)
    {
        Http::HttpRequestMessage webRequest = new(Http::HttpMethod.Get, @params.Url(this._client));
        @params.AddHeadersToRequest(webRequest, this._client);
        using Http::HttpResponseMessage response = await _client.HttpClient.SendAsync(webRequest);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Http::HttpRequestException e)
        {
            throw new Anthropic::HttpException(
                e.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        return Json::JsonSerializer.Deserialize<Json::JsonElement>(
            await response.Content.ReadAsStringAsync()
        );
    }

    public async Tasks::Task<Files::FileMetadata> RetrieveMetadata(
        Files::FileRetrieveMetadataParams @params
    )
    {
        Http::HttpRequestMessage webRequest = new(Http::HttpMethod.Get, @params.Url(this._client));
        @params.AddHeadersToRequest(webRequest, this._client);
        using Http::HttpResponseMessage response = await _client.HttpClient.SendAsync(webRequest);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Http::HttpRequestException e)
        {
            throw new Anthropic::HttpException(
                e.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        return Json::JsonSerializer.Deserialize<Files::FileMetadata>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Files::FileMetadata> Upload(Files::FileUploadParams @params)
    {
        Http::HttpRequestMessage webRequest = new(Http::HttpMethod.Post, @params.Url(this._client))
        {
            Content = @params.BodyContent(),
        };
        @params.AddHeadersToRequest(webRequest, this._client);
        using Http::HttpResponseMessage response = await _client.HttpClient.SendAsync(webRequest);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (Http::HttpRequestException e)
        {
            throw new Anthropic::HttpException(
                e.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        return Json::JsonSerializer.Deserialize<Files::FileMetadata>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }
}
