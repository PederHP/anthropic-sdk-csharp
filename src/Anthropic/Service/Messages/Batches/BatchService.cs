using Anthropic = Anthropic;
using Batches = Anthropic.Models.Messages.Batches;
using Http = System.Net.Http;
using Json = System.Text.Json;
using System = System;
using Tasks = System.Threading.Tasks;

namespace Anthropic.Service.Messages.Batches;

public sealed class BatchService : IBatchService
{
    readonly Anthropic::IAnthropicClient _client;

    public BatchService(Anthropic::IAnthropicClient client)
    {
        _client = client;
    }

    public async Tasks::Task<Batches::MessageBatch> Create(Batches::BatchCreateParams @params)
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
        return Json::JsonSerializer.Deserialize<Batches::MessageBatch>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Batches::MessageBatch> Retrieve(Batches::BatchRetrieveParams @params)
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
        return Json::JsonSerializer.Deserialize<Batches::MessageBatch>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Batches::BatchListPageResponse> List(Batches::BatchListParams @params)
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
        return Json::JsonSerializer.Deserialize<Batches::BatchListPageResponse>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Batches::DeletedMessageBatch> Delete(
        Batches::BatchDeleteParams @params
    )
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
        return Json::JsonSerializer.Deserialize<Batches::DeletedMessageBatch>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Batches::MessageBatch> Cancel(Batches::BatchCancelParams @params)
    {
        Http::HttpRequestMessage webRequest = new(Http::HttpMethod.Post, @params.Url(this._client));
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
        return Json::JsonSerializer.Deserialize<Batches::MessageBatch>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }

    public async Tasks::Task<Batches::MessageBatchIndividualResponse> Results(
        Batches::BatchResultsParams @params
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
        return Json::JsonSerializer.Deserialize<Batches::MessageBatchIndividualResponse>(
                await response.Content.ReadAsStringAsync()
            ) ?? throw new System::NullReferenceException();
    }
}
