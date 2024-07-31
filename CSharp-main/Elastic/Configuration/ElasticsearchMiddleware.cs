using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

public class ElasticsearchMiddleware<TRequest, TResponse> 
    where TRequest : class 
    where TResponse : class
{
    private readonly RequestDelegate _next;
    private readonly IElasticsearchService _elasticsearchService;

    public ElasticsearchMiddleware(RequestDelegate next, IElasticsearchService elasticsearchService)
    {
        _next = next;
        _elasticsearchService = elasticsearchService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Chamar o próximo middleware na cadeia
        await _next(context);

        // Obter o objeto do request e response
        TRequest requestObj = await GetRequestBodyAsync<TRequest>(context);
        TResponse responseObj = await GetResponseBodyAsync<TResponse>(context);

        // Obter o nome do tipo do objeto sendo usado no request
        string indexName = typeof(TRequest).Name.ToLower();

        _elasticsearchService.IndexRequestResponse(requestObj, responseObj, indexName);
    }

    private async Task<T> GetRequestBodyAsync<T>(HttpContext context)
    {
        using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
        {
            string requestBody = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }
    }

    private async Task<T> GetResponseBodyAsync<T>(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin); // Voltar ao início do corpo da resposta

        using (StreamReader reader = new StreamReader(context.Response.Body, Encoding.UTF8))
        {
            string responseBody = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
