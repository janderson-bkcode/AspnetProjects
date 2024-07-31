using br.com.sharklab.elasticsearch;

public interface IElasticsearchService
{
    void IndexRequestResponse(GenericRequestResponse<Dictionary<string, object>> requestResponse, string indexName);
}