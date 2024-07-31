using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class Produto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
}

public class ProcessamentoProdutosService : BackgroundService
{
    private readonly ConcurrentQueue<Produto> filaProdutos;

    public ProcessamentoProdutosService(ConcurrentQueue<Produto> filaProdutos)
    {
        this.filaProdutos = filaProdutos;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (filaProdutos.TryDequeue(out Produto produto))
            {
                Console.WriteLine($"Processando o produto '{produto.Nome}'...");
                // Simulando algum processamento demorado
                await Task.Delay(2000);
                Console.WriteLine($"O produto '{produto.Nome}' foi processado. Preço: {produto.Preco:C}");
            }

            await Task.Delay(1000); // Aguarda 1 segundo antes de verificar novamente a fila
        }
    }
}

public class Program
{
    private static ConcurrentQueue<Produto> filaProdutos = new ConcurrentQueue<Produto>();

    public static async Task Main(string[] args)
    {
        // Enfileirando produtos
        EnfileirarProduto(new Produto { Nome = "Produto 1", Preco = 10 });
        EnfileirarProduto(new Produto { Nome = "Produto 2", Preco = 20 });
        EnfileirarProduto(new Produto { Nome = "Produto 3", Preco = 30 });

        using var host = CreateHostBuilder(args).Build();

        await host.StartAsync();

        Console.WriteLine("Pressione qualquer tecla para sair.");
        Console.ReadKey();

        await host.StopAsync();
    }

    public static void EnfileirarProduto(Produto produto)
    {
        filaProdutos.Enqueue(produto);
        Console.WriteLine($"O produto '{produto.Nome}' foi enfileirado.");
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(filaProdutos);
                services.AddHostedService<ProcessamentoProdutosService>();
            });
}

//
