using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Emitter.Api.Configuration
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Ler o corpo do request
            string requestBody = await ReadRequestBody(context.Request);

            // Capturar o response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            // Ler o corpo do response
            string responseBodyContent = await ReadResponseBody(context.Response);

            // Registrar os logs
            _logger.LogInformation("Request: {RequestMethod} {RequestPath}", context.Request.Method, context.Request.Path);
            _logger.LogInformation("Request Headers: {@RequestHeaders}", context.Request.Headers);
            _logger.LogInformation("Request Body: {RequestBody}", requestBody);
            _logger.LogInformation("Response Body: {ResponseBody}", responseBodyContent);

            // Restaurar o corpo original do response
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
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
}

  public static class SerilogExtensions
    {
        public static void AddSerilog(IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", environment)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                    {
                        AutoRegisterTemplate = true,
                        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}{DateTime.Now.ToString("yyyy.MM.dd")}"
                    })
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
        }
    }


      public static class ElasticExtensions
    {

        public static void UseElasticApm(this IApplicationBuilder app, IConfiguration configuration)
        {
            //https://www.elastic.co/guide/en/apm/agent/dotnet/current/configuration-on-asp-net-core.html
            app.UseAllElasticApm(configuration);
        }
    }
