using Microsoft.Extensions.Options;
using Nest;

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

public interface IElasticsearchService
{
    void IndexRequestResponse<TRequest, TResponse>(TRequest request, TResponse response, string indexName)
        where TRequest : class
        where TResponse : class;
}