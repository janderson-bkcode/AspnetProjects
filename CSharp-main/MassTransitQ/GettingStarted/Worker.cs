
global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using  MassTransit;

namespace GettingStarted;

using Contracts;

using Microsoft.Extensions.Hosting;



public class Worker : BackgroundService
{
    readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new GettingStarted { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
