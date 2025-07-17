using Anthropic = Anthropic;
using Http = System.Net.Http;
using Json = System.Text.Json;
using Models = Anthropic.Models.Models;
using System = System;
using Tasks = System.Threading.Tasks;

namespace Anthropic.Service.Models;

public sealed class ModelService : IModelService
{
    readonly Anthropic::IAnthropicClient _client;

    public ModelService(Anthropic::IAnthropicClient client)
    {
        _client = client;
    }

    public async Tasks::Task<Models::ModelInfo> Retrieve(Models::ModelRetrieveParams @params)
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
        return Json::JsonSerializer.Deserialize<Models::ModelInfo>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Models::ModelListPageResponse> List(Models::ModelListParams @params)
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
        return Json::JsonSerializer.Deserialize<Models::ModelListPageResponse>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }
}
