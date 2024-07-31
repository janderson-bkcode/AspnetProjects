using br.com.sharklab.elasticsearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

public class ElasticsearchMiddleware
{
    private readonly RequestDelegate _next;

    public ElasticsearchMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        IElasticsearchService elasticserarch = context.RequestServices.GetRequiredService<IElasticsearchService>();

        string? requestObj = ReadRequestBody(context.Request).Result;

        string? responseObj = ReadResponseBody(context.Response).Result;
        var dataRequestResponse = GenerateResponse(requestObj, responseObj);

        ControllerActionDescriptor controllerActionDescriptor
                = context.Features
                .Get<IEndpointFeature>().Endpoint.Metadata
                .GetMetadata<ControllerActionDescriptor>();

        Type? requestType = GetRequestType(controllerActionDescriptor);

        string indexName = CreateIndexName(requestType);

        elasticserarch.IndexRequestResponse(dataRequestResponse, indexName);
    }

    private GenericRequestResponse<Dictionary<string, object>> GenerateResponse(string dataStringRequest, string dataStringResponse)
    {
        var jsonDictRequest = DeserializeJson(dataStringRequest);
        var jsonDictResponse = DeserializeJson(dataStringResponse);
        var response = new GenericRequestResponse<Dictionary<string, object>>()
        {
            Request = jsonDictRequest,
            Response = jsonDictResponse
        };

        return response;
    }

    public static Dictionary<string, object> DeserializeJson(string jsonString)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

        foreach (var kvp in dictionary)
        {
            if (kvp.Value is JObject nestedObject)
            {
                dictionary[kvp.Key] = DeserializeJson(nestedObject.ToString());
            }
        }

        return dictionary;
    }
    private string CreateIndexName(Type type)
    {
        return type.Name.ToLower();
    }

    private Type GetRequestType(ControllerActionDescriptor actionDescriptor)
    {
        var parameters = actionDescriptor?.MethodInfo.GetParameters();

        var requestParameter = parameters?.FirstOrDefault();

        return requestParameter?.ParameterType;
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.Body.Seek(0, SeekOrigin.Begin);
        using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
        {
            string body = await reader.ReadToEndAsync();
            return body;
        }
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string responseBody = await new StreamReader(response.Body, encoding: Encoding.UTF8).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return responseBody;
    }
}