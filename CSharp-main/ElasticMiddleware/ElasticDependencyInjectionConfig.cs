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