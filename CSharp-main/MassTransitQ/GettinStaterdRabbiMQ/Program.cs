using GettingStartedRabbiMQ;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GettinStaterdRabbiMQ
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureServices((hostContext, services) =>
         {
             services.AddMassTransit(x =>
             {
                 // elided...

                 x.UsingRabbitMq((context, cfg) =>
                 {
                     cfg.Host("localhost", "/", h =>
                     {
                         h.Username("guest");
                         h.Password("guest");
                     });

                     cfg.ConfigureEndpoints(context);
                 });
             });

             services.AddHostedService<Worker>();
         });

    }
}
