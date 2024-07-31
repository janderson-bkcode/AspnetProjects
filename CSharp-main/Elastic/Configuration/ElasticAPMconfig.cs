using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elastic.Configuration
{
     public static class ElasticExtensions
    {

        public static void UseElasticApm(this IApplicationBuilder app, IConfiguration configuration)
        {
            //https://www.elastic.co/guide/en/apm/agent/dotnet/current/configuration-on-asp-net-core.html
            app.UseAllElasticApm(configuration);
        }
    }
}