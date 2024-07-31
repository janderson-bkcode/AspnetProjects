using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoRazorBKBank.Data;
using ProjetoRazorBKBank.Interfaces.Db;

namespace ProjetoRazorBKBank.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>(); 
            
            return services;
        }
    }
}