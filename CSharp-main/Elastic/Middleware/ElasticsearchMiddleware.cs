using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elastic.Middleware
{
   public class ElasticsearchMiddleware
{
    private readonly RequestDelegate _next;

    public ElasticsearchMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IElasticsearchService elasticsearch)
    {
        await _next(context);

        var requestObj = await GetBodyAsync(context.Request.Body);
        var responseObj = await GetBodyAsync(context.Response.Body);

        var controllerActionDescriptor = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

        var requestType = GetRequestType(controllerActionDescriptor);
        var indexName = requestType?.Name.ToLower();

        elasticsearch.IndexRequestResponse(requestObj, responseObj, indexName);
    }

    private async Task<object> GetBodyAsync(Stream bodyStream)
    {
        bodyStream.Seek(0, SeekOrigin.Begin);
        using (StreamReader reader = new StreamReader(bodyStream, Encoding.UTF8))
        {
            string body = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject(body);
        }
    }

    private Type GetRequestType(ControllerActionDescriptor actionDescriptor)
    {
        var parameters = actionDescriptor?.MethodInfo.GetParameters();
        var requestParameter = parameters?.FirstOrDefault();
        return requestParameter?.ParameterType;
    }
}
}


using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;

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

    public void IndexRequestResponse(object request, object response, string indexName)
    {
        if (!_elasticClient.Indices.Exists(indexName.ToLower()).Exists)
            _elasticClient.Indices.Create(indexName.ToLower());

        var bulkDescriptor = new BulkDescriptor();
        bulkDescriptor.Index<object>(idx => idx.Document(request).Index(indexName));
        bulkDescriptor.Index<object>(idx => idx.Document(response).Index(indexName));

        var bulkResponse = _elasticClient.Bulk(bulkDescriptor);

        if (bulkResponse != null && !bulkResponse.IsValid)
        {
            if (bulkResponse.OriginalException != null)
            {
                throw new Exception(bulkResponse.OriginalException.ToString());
            }
            else
            {
                throw new Exception("Unknown bulk response error.");
            }
        }
    }

}

public interface IElasticsearchService
{
    void IndexRequestResponse(object request, object response, string indexName);
}

public class ElasticConfiguration
{
    public string Uri { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DefaultIndex { get; set; }
}

using Microsoft.Extensions.DependencyInjection;

namespace br.com.sharklab.elasticsearch
{
    public static class ElasticDependencyInjectionConfig
    {
        public static IServiceCollection AddElasticServiceDI(this IServiceCollection services)
        {
            services.AddScoped<IElasticsearchService, ElasticsearchService>();

            return services;
        }
    }
}