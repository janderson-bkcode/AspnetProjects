using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Nest;
using System.Text;

public class ElasticsearchMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IElasticsearchService _elasticsearchService;

    public ElasticsearchMiddleware(RequestDelegate next, IElasticsearchService elasticsearchService)
    {
        _next = next;
        _elasticsearchService = elasticsearchService;
    }

    public async Task Invoke(HttpContext context)
    {
        // Antes de chamar o próximo middleware, você pode acessar o request e response aqui
        var requestBody = await ReadRequestBody(context.Request);
        var response = context.Response.Body;

        // Capturar o response
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        // Chame o próximo middleware na pipeline
        await _next(context);

        // Ler o corpo do response
        string responseBodyContent = await ReadResponseBody(context.Response);

        // Após o próximo middleware, você pode indexar o request e response no Elasticsearch
        _elasticsearchService.IndexRequestResponse(requestBody, responseBodyContent, context.Request.GetType().Name.ToLower());
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        string requestBody = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return requestBody;
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string responseBody = await new StreamReader(response.Body, encoding: Encoding.UTF8).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return responseBody;
    }
}

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticClient _elasticClient;

    public ElasticsearchService(IOptions<ElasticConfiguration> elasticsearchSettings)
    {
        var uri = new Uri(elasticsearchSettings.Value.Uri);
        var username = elasticsearchSettings.Value.Username;
        var password = elasticsearchSettings.Value.Password;

        var connectionSettings = new ConnectionSettings(uri)
            .DefaultIndex(elasticsearchSettings.Value.DefaultIndex)
            .BasicAuthentication(username, password);

        _elasticClient = new ElasticClient(connectionSettings);
    }

    public void IndexRequestResponse<TRequest, TResponse>(TRequest request, TResponse response, string indexName)
        where TRequest : class
        where TResponse : class
    {
        if (!_elasticClient.Indices.Exists(indexName.ToLower()).Exists)
            _elasticClient.Indices.Create(indexName.ToLower());
        var descriptor = new BulkDescriptor();
        descriptor.Index<TRequest>(idx => idx
            .Document(request)
            .Index(indexName));
        descriptor.Index<TResponse>(idx => idx
            .Document(response)
            .Index(indexName));

        var bulkResponse = _elasticClient.Bulk(descriptor);

        if (!bulkResponse.IsValid)
        {
            throw new Exception(bulkResponse.OriginalException.ToString());
        }
    }
}

public class ElasticConfiguration
{
    public string Uri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DefaultIndex { get; set; }
}

public interface IElasticsearchService
{
    void IndexRequestResponse<TRequest, TResponse>(TRequest request, TResponse response, string indexName)
        where TRequest : class
        where TResponse : class;
}