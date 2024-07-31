using br.com.sharklab.elasticsearch;
using Microsoft.Extensions.Options;
using Nest;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticClient _elasticClient;

    public ElasticsearchService(IOptions<ElasticConfiguration> elasticsearchSettings)
    {
        var uri = new Uri(elasticsearchSettings.Value.Uri);
        var connectionSettings = new ConnectionSettings(uri)
            .DefaultIndex(elasticsearchSettings.Value.DefaultIndex).
            BasicAuthentication(elasticsearchSettings.Value.Username, elasticsearchSettings.Value.Password);

        _elasticClient = new ElasticClient(connectionSettings);
    }

    public void IndexRequestResponse(GenericRequestResponse<Dictionary<string, object>> RequestResponse, string indexName)
    {
        if (!_elasticClient.Indices.Exists(indexName.ToLower()).Exists)
            _elasticClient.Indices.Create(indexName.ToLower());
        var bulkDescriptor = new BulkDescriptor();
        bulkDescriptor.Index<GenericRequestResponse<Dictionary<string, object>>>(idx => idx.Document(RequestResponse).Index(indexName));

        var bulkResponse = _elasticClient.Bulk(bulkDescriptor);

        if (bulkResponse != null && !bulkResponse.IsValid)
        {
            if (bulkResponse.OriginalException != null)
            {
                throw new Exception(bulkResponse.OriginalException.ToString());
            }
            else
            {
                throw new Exception($"Erro ao tentar indexar os dados {bulkResponse.Errors.ToString()}");
            }
        }
    }
}